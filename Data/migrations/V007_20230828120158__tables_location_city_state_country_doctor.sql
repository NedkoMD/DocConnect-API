-- Target Database ---------------------------------------------
USE `docconnect`;
-- -------------------------------------------------------------


-- CREATE TABLE "country" --------------------------------------
CREATE TABLE `country`( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`name` VarChar( 255 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
	`alpha-2` VarChar( 2 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
	`alpha-3` VarChar( 3 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
	PRIMARY KEY ( `id` ),
	CONSTRAINT `unique_id` UNIQUE( `id` ) )
ENGINE = InnoDB
AUTO_INCREMENT = 1;
-- -------------------------------------------------------------


-- CREATE TABLE "location" -------------------------------------
CREATE TABLE `location`( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`address` VarChar( 255 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
	`city_id` Int UNSIGNED NOT NULL,
	`state_id` Int UNSIGNED NOT NULL,
	`zip` Int NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
	PRIMARY KEY ( `id` ),
	CONSTRAINT `unique_id` UNIQUE( `id` ) )
ENGINE = InnoDB
AUTO_INCREMENT = 5301;
-- -------------------------------------------------------------


-- CREATE TABLE "state" ----------------------------------------
CREATE TABLE `state`( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`ansi` VarChar( 2 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
	`name` VarChar( 51 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
	`country_id` Int UNSIGNED NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
	PRIMARY KEY ( `id` ) )
ENGINE = InnoDB
AUTO_INCREMENT = 56;
-- -------------------------------------------------------------


-- CREATE TABLE "city" -----------------------------------------
CREATE TABLE `city`( 
	`id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
	`name` VarChar( 51 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
	`state_id` Int UNSIGNED NOT NULL,
	`utc_difference` Int NOT NULL,
	`time_zone_loc` VarChar( 255 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
	`time_zone` VarChar( 5 ) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
	PRIMARY KEY ( `id` ),
	CONSTRAINT `unique_id` UNIQUE( `id` ) 
) ENGINE = InnoDB;
-- -------------------------------------------------------------


-- CREATE TABLE "doctor" ---------------------------------------
CREATE TABLE IF NOT EXISTS `doctor`( 
    `id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
    `email` VarChar(255) NOT NULL,
    `first_name` VarChar(50) NOT NULL,
    `last_name` VarChar(50) NOT NULL,
    `password_hash` VarChar(255) NOT NULL,
    `location_id` Int UNSIGNED NOT NULL,
    `picture_location` Text NULL DEFAULT NULL,
    `speciality_id` Int UNSIGNED NULL,
    `summary` Text NULL DEFAULT NULL,    
    `experience_since` Year NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY (`id`),
    CONSTRAINT `unique_id` UNIQUE (`id`)
) ENGINE = InnoDB;
-- -------------------------------------------------------------


-- CREATE FIELD "doctor_id" ------------------------------------
ALTER TABLE `user` ADD COLUMN `doctor_id` Int( 255 ) UNSIGNED NULL;
-- -------------------------------------------------------------


-- CREATE LINK "lnk_doctor_user" -------------------------------
ALTER TABLE `user`
	ADD CONSTRAINT `lnk_doctor_user` FOREIGN KEY ( `doctor_id` )
	REFERENCES `doctor`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------


-- CREATE TRIGGER "insert_doctor_specialist_trigger" ----------------------
CREATE TRIGGER `insert_doctor_user_trigger`
	AFTER INSERT ON `doctor`
    FOR EACH ROW
	INSERT INTO `user` (`doctor_id`, `password_hash`, `email`, `first_name`, `last_name`, `access_failed_count`, `email_is_confirmed`,`lockout_is_enabled`, `phone_number_is_confirmed`, `two_factor_is_enabled`, `username`) 
	VALUES (NEW.`id`, NEW.`password_hash`, NEW.`email`, NEW.`first_name`, NEW.`last_name`, 0, 1, 0, 1, 0, NEW.`email`);
-- -------------------------------------------------------------


-- CREATE TRIGGER "insert_doctor_specialist_trigger" ----------------------
CREATE TRIGGER `insert_doctor_specialist_trigger`
	AFTER INSERT ON `doctor`
    FOR EACH ROW
	INSERT INTO `specialist` ( `doctor_id`, `speciality_id`)
		VALUES (NEW.id, NEW.speciality_id);
-- -------------------------------------------------------------


-- CREATE INDEX "index_city_id" --------------------------------
CREATE INDEX `index_city_id` USING BTREE ON `location`( `city_id` );
-- -------------------------------------------------------------

-- CREATE INDEX "index_state_id" -------------------------------
CREATE INDEX `index_state_id` USING BTREE ON `location`( `state_id` );
-- -------------------------------------------------------------

-- CREATE INDEX "index_country_id" -----------------------------
CREATE INDEX `index_country_id` USING BTREE ON `state`( `country_id` );
-- -------------------------------------------------------------

-- CREATE INDEX "city_state_link" ------------------------------
CREATE INDEX `city_state_link` USING BTREE ON `city`( `state_id` );
-- -------------------------------------------------------------

-- CREATE INDEX "index_location_id" ----------------------------
CREATE INDEX `index_location_id` ON `doctor`( `location_id` );
-- -------------------------------------------------------------


-- CREATE LINK "lnk_location_doctor" ---------------------------
ALTER TABLE `doctor`
	ADD CONSTRAINT `lnk_location_doctor` FOREIGN KEY ( `location_id` )
	REFERENCES `location`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------


-- CREATE LINK "lnk_doctor_specialist" -------------------------
ALTER TABLE `specialist`
	ADD CONSTRAINT `lnk_doctor_specialist` FOREIGN KEY ( `doctor_id` )
	REFERENCES `doctor`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------


-- CREATE LINK "location_city_link" ----------------------------
ALTER TABLE `location`
	ADD CONSTRAINT `location_city_link` FOREIGN KEY ( `city_id` )
	REFERENCES `city`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------


-- CREATE LINK "location_state_link" ---------------------------
ALTER TABLE `location`
	ADD CONSTRAINT `location_state_link` FOREIGN KEY ( `state_id` )
	REFERENCES `state`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------


-- CREATE LINK "city_state_link" -------------------------------
ALTER TABLE `city`
	ADD CONSTRAINT `city_state_link` FOREIGN KEY ( `state_id` )
	REFERENCES `state`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------


-- CREATE LINK "state_country_link" ----------------------------
ALTER TABLE `state`
	ADD CONSTRAINT `state_country_link` FOREIGN KEY ( `country_id` )
	REFERENCES `country`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;
-- -------------------------------------------------------------
