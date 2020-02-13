CREATE TABLE [dbo].[server_response_log] (
    [ResponseID]   UNIQUEIDENTIFIER NOT NULL,
    [StartTime]    DATETIME         NULL,
    [EndTime]      DATETIME         NULL,
    [StatusCode]   INT              NULL,
    [ResponseText] NVARCHAR (MAX)   NULL,
    [ErrorCode]    INT              NULL,
    CONSTRAINT [PK_server_response_log] PRIMARY KEY CLUSTERED ([ResponseID] ASC)
);

