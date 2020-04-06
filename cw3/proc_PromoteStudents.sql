ALTER PROCEDURE PromoteStudents @Studies NVARCHAR(100), @Semester INT
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRAN
	DECLARE @IdStudy INT = (SELECT IdStudy FROM Studies WHERE Name = @Studies);
	IF @IdStudy IS NULL
	BEGIN
		ROLLBACK;
		RAISERROR ( 'Not found',1,1)
		RETURN;
	END;
	DECLARE @IdEnrollment INT = (SELECT IdEnrollment FROM Enrollment WHERE IdStudy = @IdStudy AND Semester = @Semester);
	IF @IdEnrollment IS NULL
	BEGIN
		ROLLBACK;
		RAISERROR ( 'Not found',1,1);
		RETURN;
	END;
	DECLARE @IdEnrollmentInserting INT = (SELECT IdEnrollment FROM Enrollment WHERE IdStudy = @IdStudy AND Semester = @Semester + 1);
	IF @IdEnrollmentInserting IS NULL
	BEGIN
		SET @IdEnrollmentInserting = (SELECT MAX(IdEnrollment) FROM Enrollment) + 1;
		DECLARE @DateNow NVARCHAR(100) = (SELECT CONVERT (date, GETDATE()));
		INSERT INTO Enrollment VALUES(@IdEnrollmentInserting, @Semester + 1, @IdStudy, @DateNow);
	END;
	UPDATE Student SET IdEnrollment = @IdEnrollmentInserting WHERE IdEnrollment = @IdEnrollment;
	COMMIT tran;
END

GO;