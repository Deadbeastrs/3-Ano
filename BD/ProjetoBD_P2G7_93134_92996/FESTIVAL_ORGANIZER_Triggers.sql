GO
CREATE TRIGGER CheckVenueAvailable ON dbo.FESTIVAL
AFTER INSERT , UPDATE
AS
BEGIN
	DECLARE @FestivalReference CHAR(9);
	DECLARE @DateStart DATETIME;
	DECLARE @DateEnd DATETIME;
	SELECT @FestivalReference = REFERENCE, @DateStart = [START_DATE], @DateEnd = [END_DATE] FROM inserted;
	IF @DateStart < CURRENT_TIMESTAMP OR @DateEnd < CURRENT_TIMESTAMP OR @DateEnd < @DateStart
		BEGIN
		RAISERROR('Data Invalida, deve ser maior que o dia de hoje ', 16, 1);
		ROLLBACK TRAN;
		END
END
GO

CREATE TRIGGER CheckVenueAlreadyOccupied ON dbo.FESTIVAL
AFTER INSERT , UPDATE
AS
BEGIN
    DECLARE @start as datetime
    DECLARE @end as datetime
    DECLARE @venue_loc as VARCHAR(256)
    DECLARE @counter as int
    SELECT  @venue_loc=LOCATION_VENUE, @start = [START_DATE], @end = [END_DATE] FROM inserted;

    SELECT @counter = COUNT(REFERENCE) FROM FESTIVAL WHERE LOCATION_VENUE LIKE @venue_loc AND (([START_DATE] <= @start AND [END_DATE] >= @start) OR ([START_DATE] <= @end AND [END_DATE] >= @end) OR ([START_DATE] >= @start AND [END_DATE] <= @end))
	
	IF @counter > 1
       BEGIN
       RAISERROR('Outro festival já vai ocurrer neste local ao mesmo tempo', 16, 1);
       ROLLBACK TRAN;
       END
END
GO

CREATE TRIGGER CheckStaffAlreadyWorking ON dbo.WORKS_ON
INSTEAD OF INSERT , UPDATE
AS
BEGIN
    DECLARE @counter as int
    DECLARE @nif_s as int
    DECLARE @start as datetime
    DECLARE @end as datetime
    DECLARE @fest_ref as char(9)


    SELECT @nif_s = NIF, @fest_ref = [FESTIVAL_REFERENCE] FROM inserted
    SELECT @start = [START_DATE], @end = [END_DATE] FROM FESTIVAL WHERE REFERENCE LIKE @fest_ref

    SELECT @counter = COUNT(FESTIVAL_REFERENCE) FROM (WORKS_ON JOIN FESTIVAL ON FESTIVAL_REFERENCE=REFERENCE) WHERE NIF LIKE @nif_s AND (([START_DATE] <= @start AND [END_DATE] >= @start) OR ([START_DATE] <= @end AND [END_DATE] >= @end) OR ([START_DATE] >= @start AND [END_DATE] <= @end))

    -- Update, Insert or Delete?
    DECLARE @Action as char(1);
    SET @Action = (CASE WHEN EXISTS(SELECT * FROM INSERTED)
                         AND EXISTS(SELECT * FROM DELETED)
                        THEN 'U'  -- Set Action to Updated.
                        WHEN EXISTS(SELECT * FROM INSERTED)
                        THEN 'I'  -- Set Action to Insert.
                        WHEN EXISTS(SELECT * FROM DELETED)
                        THEN 'D'  -- Set Action to Deleted.
                        ELSE NULL -- Skip. It may have been a "failed delete".
                    END)
    IF @Action = 'I'
        INSERT INTO WORKS_ON SELECT  * FROM inserted

    IF @counter > 0
       RAISERROR('O staff já se encontra a trabalhar noutro festival á mesma hora', 16, 1);

END
GO

CREATE TRIGGER CheckFestivalConcertSchedule ON dbo.CONCERT
AFTER INSERT , UPDATE
AS
BEGIN
    DECLARE @duration_concerts as time
    DECLARE @duration_concerts_date_time as datetime
    DECLARE @start_festival as datetime
    DECLARE @end_festival as datetime
    DECLARE @fest_ref as char(9)
	DECLARE @stage as int;


    SELECT @fest_ref = FESTIVAL_REFERENCE,@stage = STAGE_ID FROM inserted

    SELECT @start_festival = [START_DATE], @end_festival = [END_DATE] FROM FESTIVAL WHERE REFERENCE LIKE @fest_ref
    --SELECT @duration_concerts = SUM(DURATION) FROM CONCERT WHERE FESTIVAL_REFERENCE LIKE @fest_ref GROUP BY FESTIVAL_REFERENCE
    SELECT @duration_concerts = DATEADD(ms, SUM(DATEDIFF(ms, 0, DURATION)), 0) FROM CONCERT WHERE FESTIVAL_REFERENCE LIKE @fest_ref AND STAGE_ID = @stage
    SELECT @duration_concerts_date_time = CAST(@duration_concerts AS datetime)
    IF (@duration_concerts_date_time + @start_festival) > @end_festival
        BEGIN
        RAISERROR('Não e possivel adicionar o concerto, o tempo dos concertos excedem o tempo do festival ', 16, 1);
        ROLLBACK TRAN;
        END

END
GO