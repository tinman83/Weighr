CREATE DATABASE WeighR;


CREATE TABLE [TransactionLogs] (
  [TransactionLogId] INTEGER IDENTITY(1,1) PRIMARY KEY NOT NULL
, [ActualWeight] Decimal(18,4) NOT NULL
, [BatchCode] varchar(50) NULL
, [DateUploaded] DateTime NOT NULL
, [DeviceId] varchar(100) NULL
, [LineNumber] varchar(50) NULL
, [OrderNumber] varchar(50) NULL
, [ProductCode] varchar(50) NULL
, [ProductDensity] Decimal(18,4) NOT NULL
, [ProductId] bigint  NOT NULL
, [ShiftId] bigint  NOT NULL
, [TargetWeight] Decimal(18,4) NOT NULL
, [TransactionDate] DateTime NOT NULL
, [Units] int NULL
, [Uploaded] bit  NOT NULL
, [WeightDifference] Decimal(18,4) NOT NULL
, [WeightStatus] int  NOT NULL
, [WeightType] varchar(50) NULL
, [rowguid] uniqueidentifier NOT NULL
);
CREATE TABLE [ShiftTargets] (
  [ShiftTargetId] INTEGER IDENTITY(1,1) PRIMARY KEY  NOT NULL
, [OrderId] int  NOT NULL
, [OrderNumber] varchar(50) NULL
, [ProductId] int  NOT NULL
, [ShiftDate] DateTime NOT NULL
, [ShiftId] int  NOT NULL
, [TargetUnits] int  NOT NULL
, [UnitsDone] int  NOT NULL
);
CREATE TABLE [Shifts] (
  [ShiftId] INTEGER IDENTITY(1,1) PRIMARY KEY NOT NULL
, [EndTime] DateTime NOT NULL
, [Name] varchar(100) NULL
, [StartTime] DateTime NOT NULL
);
CREATE TABLE [ScaleSettings] (
  [ScaleSettingId] INTEGER  IDENTITY(1,1) PRIMARY KEY NOT NULL
, [DecimalPointPositionName] varchar(50) NULL
, [DecimalPointPrecision] int  NOT NULL
, [Density] Decimal(18,4) NOT NULL
, [DisplayUnits] varchar(50) NULL
, [DisplayUnitsWeight] Decimal(18,4) NOT NULL
, [Inflight] Decimal(18,4) NOT NULL
, [InflightTiming] int  NOT NULL
, [LowerLimit] Decimal(18,4) NOT NULL
, [MaximumCapacity] Decimal(18,4) NOT NULL
, [MinimumDivision] Decimal(18,4) NOT NULL
, [PrintMode] int  NOT NULL
, [UpperLimit] Decimal(18,4) NOT NULL
, [ZeroRange] Decimal(18,4) NOT NULL
,pushToCloud bit
,iotHubUri varchar(1000) 
,iotHubDeviceKey varchar(100) 
,pushToWebApi bit 
,ServerUrl varchar(1000)

);
CREATE TABLE [ScaleConfigs] (
  [ScaleConfigId] INTEGER  IDENTITY(1,1) PRIMARY KEY NOT NULL
, [Gradient] Decimal(18,15) NOT NULL
, [Resolution] Decimal(18,6) NOT NULL
, [YIntercept] Decimal(18,6) NOT NULL
, [offset] Decimal(18,4) NOT NULL
);
CREATE TABLE [Products] (
  [ProductId] INTEGER IDENTITY(1,1) PRIMARY KEY  NOT NULL
, [Density] Decimal(18,4) NOT NULL
, [DribblePoint] Decimal(18,4) NOT NULL
, [Inflight] Decimal(18,4) NOT NULL
, [LowerLimit] Decimal(18,4) NOT NULL
, [Name] varchar(100) NULL
, [ProductCode] varchar(50) NULL
, [TargetWeight] Decimal(18,4) NOT NULL
, [UpperLimit] Decimal(18,4) NOT NULL
, [isCurrent] bit  NOT NULL
);
CREATE TABLE [Orders] (
  [OrderId] INTEGER IDENTITY(1,1) PRIMARY KEY  NOT NULL
, [DueDate] DateTime NOT NULL
, [OrderDate] DateTime NOT NULL
, [OrderNumber] varchar(50) NULL
, [Status] bit  NOT NULL
, [rowguid] uniqueidentifier NOT NULL
);
CREATE TABLE [OrderDetails] (
  [OrderDetailId] INTEGER IDENTITY(1,1) PRIMARY KEY  NOT NULL
, [OrderId] int  NOT NULL
, [ProductId] int  NOT NULL
, [Units] int  NOT NULL
);
CREATE TABLE [Batches] (
  [BatchId] INTEGER IDENTITY(1,1) PRIMARY KEY NOT NULL
, [BatchCode] varchar(50) NULL
, [EndTime] DateTime NOT NULL
, [StartTime] DateTime NOT NULL
, [isCurrent] bit  NOT NULL
);
CREATE TABLE [AccumulatedWeights] (
  [AccumulatedWeightId] INTEGER IDENTITY(1,1) PRIMARY KEY  NOT NULL
, [CurrentDate] DateTime NOT NULL
, [StartDate] DateTime NOT NULL
, [Weight] Decimal(18,4) NOT NULL
);

