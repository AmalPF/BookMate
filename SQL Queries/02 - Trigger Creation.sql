
USE BookMateDB

CREATE TRIGGER BookRatingUpdation
    ON Rating
    FOR INSERT, UPDATE
    AS
    BEGIN
		DECLARE @bid INT;
		SELECT @bid = BId FROM inserted;
		UPDATE Books SET BRating = (SELECT AVG(RRating) FROM Rating WHERE BId = @bid) WHERE BId = @bid;
		PRINT 'New Rating Updated';
    END

CREATE TRIGGER BookRatingDeletion
    ON Rating
    FOR DELETE
    AS
    BEGIN
		DECLARE @bid INT;
		SELECT @bid = BId FROM deleted;
		UPDATE Books SET BRating = (SELECT AVG(RRating) FROM Rating WHERE BId = @bid) WHERE BId = @bid;
		PRINT 'New Rating Updated';
    END

CREATE TRIGGER NPurchasesIncrement
    ON Purchase
    FOR INSERT
    AS
    BEGIN
		DECLARE @bid INT;
		DECLARE @PCount INT;
		SELECT @PCount = PQuantity, @bid = BId FROM inserted;
		UPDATE Books SET BNPurchases = BNPurchases + @PCount, BQuantity = BQuantity - @PCount WHERE BId = @bid;
		PRINT 'Number of Purchases Updated';
    END

CREATE TRIGGER ItemRemovedCart
    ON Purchase
    FOR INSERT
    AS
    BEGIN
		DECLARE @bid INT;
		DECLARE @uid INT;
		SELECT @bid = BId FROM inserted;
		SELECT @uid = UId FROM inserted;
		DELETE FROM Cart WHERE BId = @bid AND UId = @uid;
		PRINT 'Purchased Item removed from Cart';
    END

--DROP TRIGGER BookRatingUpdation