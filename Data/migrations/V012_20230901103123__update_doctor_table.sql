-- Target Database ---------------------------------------------
USE `docconnect`;
-- -------------------------------------------------------------


-- DOCTOR TABLE UPDATE -----------------------------------------

-- CREATE FIELD "user_id" --------------------------------------
ALTER TABLE `doctor` ADD COLUMN `user_id` Int UNSIGNED NULL DEFAULT NULL;

-- CREATE UNIQUE "unique_user_id" ------------------------------
ALTER TABLE `doctor` ADD CONSTRAINT `unique_user_id` UNIQUE( `user_id` );

-- DOCTOR USER_ID ----------------------------------------------
UPDATE `doctor`, `user`
SET `doctor`.`user_id` = `user`.`id`
where `doctor`.`id` = `user`.`doctor_id`;

-- CREATE LINK "user_doctor_link" ------------------------------
ALTER TABLE `doctor`
	ADD CONSTRAINT `user_doctor_link` FOREIGN KEY ( `user_id` )
	REFERENCES `user`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;

-- DROP REDUNDANT FIELDS ---------------------------------------
ALTER TABLE `doctor` DROP COLUMN `email`;
ALTER TABLE `doctor` DROP COLUMN `first_name`;
ALTER TABLE `doctor` DROP COLUMN `last_name`;
ALTER TABLE `doctor` DROP COLUMN `password_hash`;
ALTER TABLE `doctor` DROP COLUMN `speciality_id`;

-- DROP TRIGGER "insert_doctor_user_trigger" -------------------
DROP TRIGGER IF EXISTS `insert_doctor_user_trigger`;

-- DROP LINK "location_doctor_link" ----------------------------
ALTER TABLE `doctor` DROP FOREIGN KEY `lnk_location_doctor`;

-- CREATE LINK "location_doctor_link" --------------------------
ALTER TABLE `doctor`
	ADD CONSTRAINT `location_doctor_link` FOREIGN KEY ( `location_id` )
	REFERENCES `location`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;


-- -------------------------------------------------------------


-- DROP LINK "lnk_doctor_user_link" ----------------------------
ALTER TABLE `user` DROP FOREIGN KEY `lnk_doctor_user`;
-- -------------------------------------------------------------


-- DROP LINK "doctor_specialist_link" --------------------------
ALTER TABLE `specialist` DROP FOREIGN KEY `lnk_doctor_specialist`;
-- -------------------------------------------------------------

-- CREATE LINK "doctor_specialist_link" ------------------------
ALTER TABLE `specialist`
	ADD CONSTRAINT `doctor_specialist_link` FOREIGN KEY ( `doctor_id` )
	REFERENCES `doctor`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------
