-- =============================================
-- Author:		Kyle Swanson
-- Create date: 2/12/2020
-- Description:	Retrieve 5 most recent responses within a timespan
-- =============================================
CREATE PROCEDURE sp_recent_responses
	@StartTime datetime,
	@EndTime datetime
AS
BEGIN
	SELECT TOP 5 StartTime, ResponseText 
	FROM server_response_log 
	WHERE StartTime > @StartTime AND StartTime <= @EndTime
	ORDER BY StartTime
END
