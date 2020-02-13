

CREATE VIEW [dbo].[vw_avg_per_hour] AS
SELECT ErrorCode, COUNT(*) / CASE WHEN DATEDIFF(hour, MIN(StartTime), MAX(StartTime)) < 1 THEN 1 ELSE DATEDIFF(hour, MIN(StartTime), MAX(StartTime)) END as avgperhour
FROM server_response_log
GROUP  BY ErrorCode
