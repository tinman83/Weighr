-- Script Date: 22/05/2018 12:05 PM  - ErikEJ.SqlCeScripting version 3.5.2.75
-- Database information:
-- Database: C:\Users\tinashe_2\Documents\Visual Studio 2017\Projects\Weighr\WeighrDAL\weighr.db
-- ServerVersion: 3.22.0
-- DatabaseSize: 64 KB
-- Created: 20/05/2018 10:37 AM

-- User Table information:
-- Number of tables: 11
-- __EFMigrationsHistory: -1 row(s)
-- AccumulatedWeights: -1 row(s)
-- Batches: -1 row(s)
-- OrderDetails: -1 row(s)
-- Orders: -1 row(s)
-- Products: -1 row(s)
-- ScaleConfigs: -1 row(s)
-- ScaleSettings: -1 row(s)
-- Shifts: -1 row(s)
-- ShiftTargets: -1 row(s)
-- TransactionLogs: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [TransactionLogs] (
  [TransactionLogId] INTEGER  NOT NULL
, [ActualWeight] text NOT NULL
, [BatchCode] text NULL
, [DateUploaded] text NOT NULL
, [DeviceId] text NULL
, [LineNumber] text NULL
, [OrderNumber] text NULL
, [ProductCode] text NULL
, [ProductDensity] text NOT NULL
, [ProductId] bigint  NOT NULL
, [ShiftId] bigint  NOT NULL
, [TargetWeight] text NOT NULL
, [TransactionDate] text NOT NULL
, [Units] text NULL
, [Uploaded] bigint  NOT NULL
, [WeightDifference] text NOT NULL
, [WeightStatus] bigint  NOT NULL
, [WeightType] text NULL
, [rowguid] image NOT NULL
, CONSTRAINT [sqlite_master_PK_TransactionLogs] PRIMARY KEY ([TransactionLogId])
);
CREATE TABLE [ShiftTargets] (
  [ShiftTargetId] INTEGER  NOT NULL
, [OrderId] bigint  NOT NULL
, [OrderNumber] text NULL
, [ProductId] bigint  NOT NULL
, [ShiftDate] text NOT NULL
, [ShiftId] bigint  NOT NULL
, [TargetUnits] bigint  NOT NULL
, [UnitsDone] bigint  NOT NULL
, CONSTRAINT [sqlite_master_PK_ShiftTargets] PRIMARY KEY ([ShiftTargetId])
);
CREATE TABLE [Shifts] (
  [ShiftId] INTEGER  NOT NULL
, [EndTime] text NOT NULL
, [Name] text NULL
, [StartTime] text NOT NULL
, CONSTRAINT [sqlite_master_PK_Shifts] PRIMARY KEY ([ShiftId])
);
CREATE TABLE [ScaleSettings] (
  [ScaleSettingId] INTEGER  NOT NULL
, [DecimalPointPositionName] text NULL
, [DecimalPointPrecision] bigint  NOT NULL
, [Density] text NOT NULL
, [DisplayUnits] text NULL
, [DisplayUnitsWeight] text NOT NULL
, [Inflight] text NOT NULL
, [InflightTiming] bigint  NOT NULL
, [LowerLimit] text NOT NULL
, [MaximumCapacity] text NOT NULL
, [MinimumDivision] text NOT NULL
, [PrintMode] bigint  NOT NULL
, [UpperLimit] text NOT NULL
, [ZeroRange] text NOT NULL
, CONSTRAINT [sqlite_master_PK_ScaleSettings] PRIMARY KEY ([ScaleSettingId])
);
CREATE TABLE [ScaleConfigs] (
  [ScaleConfigId] INTEGER  NOT NULL
, [Gradient] text NOT NULL
, [Resolution] text NOT NULL
, [YIntercept] text NOT NULL
, [offset] text NOT NULL
, CONSTRAINT [sqlite_master_PK_ScaleConfigs] PRIMARY KEY ([ScaleConfigId])
);
CREATE TABLE [Products] (
  [ProductId] INTEGER  NOT NULL
, [Density] text NOT NULL
, [DribblePoint] text NOT NULL
, [Inflight] text NOT NULL
, [LowerLimit] text NOT NULL
, [Name] text NULL
, [ProductCode] text NULL
, [TargetWeight] text NOT NULL
, [UpperLimit] text NOT NULL
, [isCurrent] bigint  NOT NULL
, CONSTRAINT [sqlite_master_PK_Products] PRIMARY KEY ([ProductId])
);
CREATE TABLE [Orders] (
  [OrderId] INTEGER  NOT NULL
, [DueDate] text NOT NULL
, [OrderDate] text NOT NULL
, [OrderNumber] text NULL
, [Status] bigint  NOT NULL
, [rowguid] image NOT NULL
, CONSTRAINT [sqlite_master_PK_Orders] PRIMARY KEY ([OrderId])
);
CREATE TABLE [OrderDetails] (
  [OrderDetailId] INTEGER  NOT NULL
, [OrderId] bigint  NOT NULL
, [ProductId] bigint  NOT NULL
, [Units] bigint  NOT NULL
, CONSTRAINT [sqlite_master_PK_OrderDetails] PRIMARY KEY ([OrderDetailId])
);
CREATE TABLE [Batches] (
  [BatchId] INTEGER  NOT NULL
, [BatchCode] text NULL
, [EndTime] text NOT NULL
, [StartTime] text NOT NULL
, [isCurrent] bigint  NOT NULL
, CONSTRAINT [sqlite_master_PK_Batches] PRIMARY KEY ([BatchId])
);
CREATE TABLE [AccumulatedWeights] (
  [AccumulatedWeightId] INTEGER  NOT NULL
, [CurrentDate] text NOT NULL
, [StartDate] text NOT NULL
, [Weight] text NOT NULL
, CONSTRAINT [sqlite_master_PK_AccumulatedWeights] PRIMARY KEY ([AccumulatedWeightId])
);
COMMIT;

