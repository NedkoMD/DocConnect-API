-- Target Database ---------------------------------------------
USE `docconnect`;

-- CREATE TABLE "user_claim" ----------------------------------
CREATE TABLE `user_claim` ( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`role_id` Int UNSIGNED NOT NULL,
	`claim_type` LongText NULL,
	`claim_value` LongText NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY ( `id` ),
	CONSTRAINT `unique_id` UNIQUE( `id` )    
) CHARACTER SET=utf8mb4;
-- -------------------------------------------------------------

-- CREATE LINK "user_claim_user_link" --------------------------
ALTER TABLE `user_role`
    ADD CONSTRAINT `user_claim_user_link` FOREIGN KEY (`user_id`) 
    REFERENCES `user` (`id`) 
    ON DELETE CASCADE 
    ON UPDATE CASCADE;
-- -------------------------------------------------------------

-- ADD SYSTEM FIELDS -------------------------------------------

-- USER_ROLE TABLE ---------------------------------------------

-- CREATE FIELD "created_at" -----------------------------------
ALTER TABLE `user_role` ADD COLUMN `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP;
-- -------------------------------------------------------------

-- CREATE FIELD "updated_at" -----------------------------------
ALTER TABLE `user_role` ADD COLUMN `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP;
-- -------------------------------------------------------------

-- CREATE FIELD "is_deleted" -----------------------------------
ALTER TABLE `user_role` ADD COLUMN `is_deleted` Bool NOT NULL DEFAULT False;
-- -------------------------------------------------------------


-- ROLE TABLE ---------------------------------------------------


-- CREATE FIELD "created_at" -----------------------------------
ALTER TABLE `role` ADD COLUMN `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP;
-- -------------------------------------------------------------

-- CREATE FIELD "updated_at" -----------------------------------
ALTER TABLE `role` ADD COLUMN `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP;
-- -------------------------------------------------------------

-- CREATE FIELD "is_deleted" -----------------------------------
ALTER TABLE `role` ADD COLUMN `is_deleted` Bool NOT NULL DEFAULT False;
-- -------------------------------------------------------------


-- ROLE_CLAIM TABLE --------------------------------------------


-- CREATE FIELD "created_at" -----------------------------------
ALTER TABLE `role_claim` ADD COLUMN `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP;
-- -------------------------------------------------------------

-- CREATE FIELD "updated_at" -----------------------------------
ALTER TABLE `role_claim` ADD COLUMN `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP;
-- -------------------------------------------------------------

-- CREATE FIELD "is_deleted" -----------------------------------
ALTER TABLE `role_claim` ADD COLUMN `is_deleted` Bool NOT NULL DEFAULT False;
-- -------------------------------------------------------------

-- CHANGE "NULLABLE" OF "FIELD "claim_type" --------------------
ALTER TABLE `role_claim` MODIFY `claim_type` LongText NULL;
-- -------------------------------------------------------------

-- CHANGE "NULLABLE" OF "FIELD "claim_value" -------------------
ALTER TABLE `role_claim` MODIFY `claim_value` LongText NULL;
-- -------------------------------------------------------------


-- USER TABLE --------------------------------------------------


-- CREATE FIELD "username" -------------------------------------
ALTER TABLE `user` ADD COLUMN `username` VarChar(256) NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "normalized_username" --------------------------
ALTER TABLE `user` ADD COLUMN `normalized_username` VarChar(256) NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "normalized_email" -----------------------------
ALTER TABLE `user` ADD COLUMN `normalized_email` VarChar(256) NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "email_is_confirmed" ---------------------------
ALTER TABLE `user` ADD COLUMN `email_is_confirmed` Bool NOT NULL DEFAULT False;
-- -------------------------------------------------------------

-- CREATE FIELD "phone_number" ---------------------------------
ALTER TABLE `user` ADD COLUMN `phone_number` LongText NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "phone_number_is_confirmed" --------------------
ALTER TABLE `user` ADD COLUMN `phone_number_is_confirmed` Bool NOT NULL DEFAULT False;
-- -------------------------------------------------------------

-- CREATE FIELD "two_factor_is_enabled" ------------------------
ALTER TABLE `user` ADD COLUMN `two_factor_is_enabled` Bool NOT NULL DEFAULT False;
-- -------------------------------------------------------------

-- CREATE FIELD "lockout_end" ----------------------------------
ALTER TABLE `user` ADD COLUMN `lockout_end` DATETIME NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "lockout_is_enabled" ---------------------------
ALTER TABLE `user` ADD COLUMN `lockout_is_enabled` Bool NOT NULL DEFAULT False;
-- -------------------------------------------------------------

-- CREATE FIELD "access_failed_count" --------------------------
ALTER TABLE `user` ADD COLUMN `access_failed_count` int NOT NULL;
-- -------------------------------------------------------------

-- TOKEN TABLE -------------------------------------------------

-- CREATE FIELD "type" -----------------------------------------
ALTER TABLE `token` ADD COLUMN `type` LongText NOT NULL;
-- -------------------------------------------------------------

-- CHANGE "NAME" OF "FIELD "security_stamp" --------------------
ALTER TABLE `user` CHANGE `secyrty_stamp` `security_stamp` LongText NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "concurrency_stamp" ----------------------------
ALTER TABLE `user` MODIFY `concurrency_stamp` LongText NULL;
-- -------------------------------------------------------------
