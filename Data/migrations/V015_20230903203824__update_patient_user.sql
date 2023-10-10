-- Target Database ---------------------------------------------
USE `docconnect`;

-- CHANGE "NULLABLE" OF "FIELD "notes" -------------------------
ALTER TABLE `appointment` MODIFY `notes` Text  NULL;

-- CREATE TRIGGER "insert_user_patient_trigger" ----------------
CREATE TRIGGER `insert_user_patient_trigger`
	AFTER INSERT ON `user`
    FOR EACH ROW
	INSERT INTO `patient` ( `user_id`)
		VALUES (NEW.id);

-- INSERT MISSING Pateint for user -----------------------------
INSERT INTO `patient` (`user_id`)
SELECT `id` from `user`	
WHERE `id` not in (SELECT `user_id` from `patient`);
-- -------------------------------------------------------------
