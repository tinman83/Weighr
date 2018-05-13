using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Weighr.Helpers
{
   public static class GpioUtility
    {

        private static int clockPinNumber = 23;
        private static int dataPinNumber = 24;
        private static int ledPinNumber = 12;

        private static int runPinNumber = 25;
        private static int stopPinNumber = 8;
        private static int estopPinNumber = 7;
        private static int pressurePinNumber = 12;
        private static int normalFeedPinNumber = 16;
        private static int dribbleFeedPinNumber = 20;
        private static int underWeightPinNumber = 21;
        private static int overWeightPinNumber = 26;
        private static int normalWeightPinNumber = 19;

        private static GpioPin clockPin;
        private static GpioPin dataPin;
        private static GpioPin ledPin;

        private static GpioPin runPin;
        private static GpioPin stopPin;
        private static GpioPin estopPin;
        private static GpioPin pressurePin;
        private static GpioPin normalFeedPin;
        private static GpioPin dribbleFeedPin;
        private static GpioPin underWeightPin;
        private static GpioPin overWeightPin;
        private static GpioPin normalWeightPin;
        private static bool _available = false;

        public static bool isRunButtonPressed()
        {
            GpioPinValue pinValue = runPin.Read();
            if (pinValue == GpioPinValue.High)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool InitialiseGpio()
        {
            GpioController gpio = GpioController.GetDefault();

            if (null == gpio)
            {
                _available = false;
                return false;
            }
            /*
                * Initialize the clock pin and set to "Low"
                *
                * Instantiate the clock pin object
                * Write the GPIO pin value of low on the pin
                * Set the GPIO pin drive mode to output
                */
            clockPin = gpio.OpenPin(clockPinNumber, GpioSharingMode.Exclusive);
            clockPin.Write(GpioPinValue.Low);
            clockPin.SetDriveMode(GpioPinDriveMode.Output);

            ledPin = gpio.OpenPin(ledPinNumber, GpioSharingMode.Exclusive);
            ledPin.SetDriveMode(GpioPinDriveMode.Output);

            normalFeedPin = gpio.OpenPin(normalFeedPinNumber, GpioSharingMode.Exclusive);
            normalFeedPin.SetDriveMode(GpioPinDriveMode.Output);

            dribbleFeedPin = gpio.OpenPin(dribbleFeedPinNumber, GpioSharingMode.Exclusive);
            dribbleFeedPin.SetDriveMode(GpioPinDriveMode.Output);

            underWeightPin = gpio.OpenPin(underWeightPinNumber, GpioSharingMode.Exclusive);
            underWeightPin.SetDriveMode(GpioPinDriveMode.Output);

            overWeightPin = gpio.OpenPin(overWeightPinNumber, GpioSharingMode.Exclusive);
            overWeightPin.SetDriveMode(GpioPinDriveMode.Output);

            normalWeightPin = gpio.OpenPin(normalWeightPinNumber, GpioSharingMode.Exclusive);
            normalWeightPin.SetDriveMode(GpioPinDriveMode.Output);



            /*
                * Initialize the data pin and set to "Low"
                * 
                * Instantiate the data pin object
                * Set the GPIO pin drive mode to input for reading
                */
            dataPin = gpio.OpenPin(dataPinNumber, GpioSharingMode.Exclusive);
            dataPin.SetDriveMode(GpioPinDriveMode.Input);

            runPin = gpio.OpenPin(runPinNumber, GpioSharingMode.Exclusive);
            runPin.SetDriveMode(GpioPinDriveMode.Input);

            stopPin = gpio.OpenPin(stopPinNumber, GpioSharingMode.Exclusive);
            stopPin.SetDriveMode(GpioPinDriveMode.Input);

            estopPin = gpio.OpenPin(estopPinNumber, GpioSharingMode.Exclusive);
            estopPin.SetDriveMode(GpioPinDriveMode.Input);

            _available = true;
            return true;
        }

        public static void openNormalFeedValve()
        {
            normalFeedPin.Write(GpioPinValue.High);
        }
        public static void closeNormalFeedValve()
        {
            normalFeedPin.Write(GpioPinValue.Low); 
        }
        public static void openDribbleFeedValve()
        {
            dribbleFeedPin.Write(GpioPinValue.High);
        }
        public static void closeDribbleFeedValve()
        {
            dribbleFeedPin.Write(GpioPinValue.Low); 
        }

        public static void switchOnOverWeightLight()
        {
            overWeightPin.Write(GpioPinValue.High);  //switch on overweight light
            underWeightPin.Write(GpioPinValue.Low);
            normalWeightPin.Write(GpioPinValue.Low);
        }

        public static void switchOnNormalWeightLight()
        {
            normalWeightPin.Write(GpioPinValue.High);  //switch on normal weight light
            overWeightPin.Write(GpioPinValue.Low);
            underWeightPin.Write(GpioPinValue.Low);
        }

        public static void switchOnUnderWeightLight()
        {
            underWeightPin.Write(GpioPinValue.High);  //switch on underweight light
            overWeightPin.Write(GpioPinValue.Low);
            normalWeightPin.Write(GpioPinValue.Low);
        }
        public static int ReadData()
        {
            uint value = 0;
            byte[] data = new byte[4];

            // Wait for chip to become ready
            for (; GpioPinValue.Low != dataPin.Read();) ;

            // Clock in data
            data[1] = ShiftInByte();
            data[2] = ShiftInByte();
            data[3] = ShiftInByte();

            // Clock in gain of 128 for next reading
            clockPin.Write(GpioPinValue.High);
            clockPin.Write(GpioPinValue.Low);

            // Replicate the most significant bit to pad out a 32-bit signed integer
            if (0x80 == (data[1] & 0x80))
            {
                data[0] = 0xFF;
            }
            else
            {
                data[0] = 0x00;
            }

            // Construct a 32-bit signed integer
            value = (uint)((data[0] << 24) | (data[1] << 16) | (data[2] << 8) | data[3]);

            // Datasheet indicates the value is returned as a two's complement value
            // https://cdn.sparkfun.com/datasheets/Sensors/ForceFlex/hx711_english.pdf

            // flip all the bits
            value = ~value;

            // ... and add 1
            return (int)(++value);
        }

        private static byte ShiftInByte()
        {
            byte value = 0x00;

            // Convert "GpioPinValue.High" and "GpioPinValue.Low" to 1 and 0, respectively.
            // NOTE: Loop is unrolled for performance
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 7);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 6);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 5);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 4);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 3);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 2);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 1);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)dataPin.Read();
            clockPin.Write(GpioPinValue.Low);

            return value;
        }


    }
}
