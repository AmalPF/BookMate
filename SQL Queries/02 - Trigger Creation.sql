
USE BookMateDB

CREATE TRIGGER BookRatingUpdation
    ON Rating
    FOR INSERT, UPDATE
    AS
    BEGIN
		DECLARE @bid INT;
		SELECT @bid = BId FROM inserted;
		UPDATE Books SET BRating = (SELECT AVG(RRating) FROM Rating WHERE BId = @bid) WHERE BId = @bid;
		PRINT 'New Rating Updated on ';
    END

CREATE TRIGGER BookRatingDeletion
    ON Rating
    FOR DELETE
    AS
    BEGIN
		DECLARE @bid INT;
		SELECT @bid = BId FROM deleted;
		UPDATE Books SET BRating = (SELECT AVG(RRating) FROM Rating WHERE BId = @bid) WHERE BId = @bid;
		PRINT 'New Rating Updated on ';
    END

--DROP TRIGGER BookRatingUpdation