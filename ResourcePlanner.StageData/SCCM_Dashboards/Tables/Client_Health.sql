﻿CREATE TABLE [SCCM_Dashboards].[Client_Health] (
    [Client_ID]              VARCHAR (50)  NOT NULL,
    [Date_Key_Snapshot]      INT           NOT NULL,
    [Domain_Key]             INT           NOT NULL,
    [Region_Key]             INT           NOT NULL,
    [Date_Key_Hardware_Scan] INT           NOT NULL,
    [Date_Key_Software_Scan] INT           NOT NULL,
    [Date_Key_Heartbeat]     INT           NOT NULL,
    [Client_Name]            VARCHAR (100) NOT NULL,
    [Client_Type]            VARCHAR (50)  NOT NULL,
    [System_Role]            VARCHAR (50)  NOT NULL,
    [Domain]                 VARCHAR (300) NOT NULL,
    [Active_Directory_Site]  VARCHAR (200) NOT NULL,
    [Operating_System]       VARCHAR (200) NOT NULL,
    [Hardware_Scan_Date]     DATETIME      NOT NULL,
    [Software_Scan_Date]     DATETIME      NOT NULL,
    [Heartbeat_Date]         DATETIME      NOT NULL,
    [Client_Flag]            INT           NOT NULL,
    [Is_Server]              BIT           NOT NULL,
    [Is_Workstation]         BIT           NOT NULL,
    [Is_RTO_Client]          BIT           NOT NULL,
    [Client_Health_Status]   VARCHAR (20)  NOT NULL,
    [Healthy_Client]         BIT           NOT NULL,
    [Exist_In_SCCM]          BIT           NOT NULL,
    [Insert_Date]            DATETIME      NOT NULL,
    [Update_Date]            DATE          NULL
);

