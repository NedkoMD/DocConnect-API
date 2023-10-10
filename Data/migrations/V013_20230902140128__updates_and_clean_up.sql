-- Target Database ---------------------------------------------
USE `docconnect`;


-- UPDATE "speciality_specialist_link" -------------------------

-- DROP LINK "speciality_specialist_link" ----------------------
ALTER TABLE `specialist` DROP FOREIGN KEY `speciality_specialist_link`;

-- CREATE LINK "speciality_specialist_link" --------------------
ALTER TABLE `specialist`
    ADD CONSTRAINT `speciality_specialist_link` FOREIGN KEY ( `speciality_id` )
    REFERENCES `speciality`( `id` )
    ON DELETE No Action
    ON UPDATE Cascade;


-- UPDATE "created_at" and "updated_at" to NOT NULL ------------
UPDATE `city`       SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `country`    SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `doctor`     SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `location`   SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `patient`    SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `role`       SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `role_claim` SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `specialist` SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `speciality` SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `state`      SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `token`      SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `user`       SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `user_claim` SET `created_at` = NOW() WHERE created_at IS NULL;
UPDATE `user_role`  SET `created_at` = NOW() WHERE created_at IS NULL;

UPDATE `city`       SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `country`    SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `doctor`     SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `location`   SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `patient`    SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `role`       SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `role_claim` SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `specialist` SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `speciality` SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `state`      SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `token`      SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `user`       SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `user_claim` SET `updated_at` = NOW() WHERE updated_at IS NULL;
UPDATE `user_role`  SET `updated_at` = NOW() WHERE updated_at IS NULL;

-- CHANGE "NULLABLE" OF "FIELD "created_at" --------------------
ALTER TABLE `city`       MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `country`    MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `doctor`     MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `location`   MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `patient`    MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `role`       MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `role_claim` MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `specialist` MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `speciality` MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `state`      MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `token`      MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `user`       MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `user_claim` MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE `user_role`  MODIFY `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP;

-- CHANGE "NULLABLE" OF "FIELD "updated_at" --------------------
ALTER TABLE `city`       MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `country`    MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `doctor`     MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `location`   MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `patient`    MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `role`       MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `role_claim` MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `specialist` MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `speciality` MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `state`      MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `token`      MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `user`       MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `user_claim` MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
ALTER TABLE `user_role`  MODIFY `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;


-- CLEAN UP - REMOVE REDUNDANT or UNNEEDED ---------------------


-- DROP TRIGGER "insert_doctor_specialist_trigger" -------------
DROP TRIGGER IF EXISTS `insert_doctor_specialist_trigger`;

-- DROP VIEWS --------------------------------------------------
DROP VIEW IF EXISTS `patient_view`;
DROP VIEW IF EXISTS `specialties_view`;
DROP VIEW IF EXISTS `user_view`;

-- FROM PATIENT TABLE ------------------------------------------
DROP INDEX `index_city_id` ON `patient`;
DROP INDEX `index_country_id` ON `patient`;
DROP INDEX `patient_unique_email` ON `patient`;
DROP INDEX `patient_unique_user_slug` ON `patient`;
ALTER TABLE `patient` DROP COLUMN `email`;
ALTER TABLE `patient` DROP COLUMN `first_name`;
ALTER TABLE `patient` DROP COLUMN `last_name`;
ALTER TABLE `patient` DROP COLUMN `login_password`;
ALTER TABLE `patient` DROP COLUMN `city_id`;
ALTER TABLE `patient` DROP COLUMN `country_id`;
ALTER TABLE `patient` DROP COLUMN `user_slug`;
-- -------------------------------------------------------------
