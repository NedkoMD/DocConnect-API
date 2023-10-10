-- Target Database ---------------------------------------------
USE `docconnect`;
-- -------------------------------------------------------------

-- CREATE FIELD "secyrty_stamp" --------------------------------
ALTER TABLE `user` ADD COLUMN `secyrty_stamp` LongText NOT NULL;
-- -------------------------------------------------------------

-- CREATE FIELD "concurrency_stamp" ----------------------------
ALTER TABLE `user` ADD COLUMN `concurrency_stamp` LongText NOT NULL;
-- -------------------------------------------------------------

-- CREATE TABLE "token" ----------------------------------------
CREATE TABLE `token`( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`user_id` Int UNSIGNED NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
	`is_used` Bool NOT NULL,
	`valid_until` DateTime NOT NULL,
	`value` LongText NOT NULL,
	PRIMARY KEY ( `id` ),
	CONSTRAINT `unique_id` UNIQUE( `id` ) 
);
-- -------------------------------------------------------------


-- CREATE TABLE "user_role" -----------------------------------
CREATE TABLE `user_role`( 
	`user_id` Int UNSIGNED NOT NULL,
	`role_id` Int UNSIGNED NOT NULL,
	PRIMARY KEY ( `user_id`, `role_id` ) 
);
-- -------------------------------------------------------------

-- CREATE INDEX "index_user_id" --------------------------------
CREATE INDEX `index_user_id` ON `user_role`( `user_id` );
-- -------------------------------------------------------------

-- CREATE INDEX "index_role_id" --------------------------------
CREATE INDEX `index_role_id` ON `user_role`( `role_id` );
-- -------------------------------------------------------------


-- CREATE TABLE "role" ----------------------------------------
CREATE TABLE `role`( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`name` VarChar( 255 ) NOT NULL,
	`normalized_name` VarChar( 255 ) NOT NULL,
	`concurrency_stamp` LongText NOT NULL,
	PRIMARY KEY ( `id` ),
	CONSTRAINT `unique_id` UNIQUE( `id` ) 
);
-- -------------------------------------------------------------


-- CREATE TABLE "role_claim" ----------------------------------
CREATE TABLE `role_claim`( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`role_id` Int UNSIGNED NOT NULL,
	`claim_type` LongText NOT NULL,
	`claim_value` LongText NOT NULL,
	PRIMARY KEY ( `id` ),
	CONSTRAINT `unique_id` UNIQUE( `id` ) 
);
-- -------------------------------------------------------------


-- Create Links ------------------------------------------------

-- CREATE LINK "user_role_user_link" --------------------
ALTER TABLE `user_role`
    ADD CONSTRAINT `user_role_user_link` FOREIGN KEY ( `user_id` )
    REFERENCES `user`( `id` )
    ON DELETE Cascade
    ON UPDATE Cascade;

-- CREATE LINK "user_role_role_link" --------------------
ALTER TABLE `user_role`
    ADD CONSTRAINT `user_role_role_link` FOREIGN KEY ( `role_id` )
    REFERENCES `role`( `id` )
    ON DELETE Cascade
    ON UPDATE Cascade;

-- CREATE LINK "role_role_claim_link" --------------------
ALTER TABLE `role_claim`
    ADD CONSTRAINT `role_role_claim_link` FOREIGN KEY ( `role_id` )
    REFERENCES `role`( `id` )
    ON DELETE Cascade
    ON UPDATE Cascade;

-- CREATE LINK "token_user_link" -----------------------------
ALTER TABLE `token`
    ADD CONSTRAINT `token_user_link` FOREIGN KEY ( `user_id` )
    REFERENCES `user`( `id` )
    ON DELETE Cascade
    ON UPDATE Cascade;
