-- Target Database ---------------------------------------------
USE `docconnect`;


-- CREATE TABLES -----------------------------------------------

-- CREATE TABLE "speciality" -----------------------------------
CREATE TABLE IF NOT EXISTS `speciality`( 
    `id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
    `name` VarChar(255) NOT NULL,
    `image_url` VarChar(2047) NOT NULL DEFAULT "https://img.freepik.com/free-photo/asian-young-main-group-hospital-professional_1291-37.jpg?w=996&t=st=1690473556~exp=1690474156~hmac=301bdbc52496f28adad99d6e20e348f8d9dd9722fc6fe19a55adec9360da9afd",
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY (`id`)
) ENGINE = InnoDB;


-- CREATE TABLE "specialist" -----------------------------------
CREATE TABLE IF NOT EXISTS `specialist`( 
    `doctor_id` Int UNSIGNED NOT NULL,
    `speciality_id` Int UNSIGNED NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY (`doctor_id`, `speciality_id`)
) ENGINE = InnoDB;

-- CREATE INDEX "index_speciality_id" --------------------------
CREATE INDEX `index_speciality_id` ON `specialist`(`speciality_id`);

-- CREATE INDEX "index_doctor_id" ------------------------------
CREATE INDEX `index_doctor_id` ON `specialist`(`doctor_id`);


-- CREATE TABLE "user" -----------------------------------------
CREATE TABLE IF NOT EXISTS `user`( 
    `id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
    `login_password` VarChar(255) NOT NULL,
    `email` VarChar(255) NOT NULL,
    `is_verified` Bool NOT NULL DEFAULT False,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY (`id`),
    CONSTRAINT `user_unique_email` UNIQUE (`email`),
    CONSTRAINT `user_unique_id` UNIQUE (`id`),
    CONSTRAINT `user_email_validation` CHECK (`email` REGEXP "^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
) ENGINE = InnoDB;


-- CREATE TABLE "patient" --------------------------------------
CREATE TABLE IF NOT EXISTS `patient`(
    `id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
    `email` VarChar(255) NOT NULL,
    `first_name` VarChar(50) NOT NULL,
    `last_name` VarChar(50) NOT NULL,
    `weight` Float NULL COMMENT 'Weight in kg',
    `height` Float NULL COMMENT 'Height in cm',
    `blood_pressure` VarChar( 15 ) NULL,
    `blood_sugar` Float NULL COMMENT 'Blood sugar level In mmol/L',
    `login_password` VarChar(255) NOT NULL,
    `city_id` Int UNSIGNED NULL,
    `country_id` Int UNSIGNED NULL,
    `user_id` Int UNSIGNED NOT NULL,
    `user_slug` VarChar(255) NOT NULL,
    `created_at` Timestamp DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY (`id`),
    CONSTRAINT `patient_unique_email` UNIQUE (`email`),
    CONSTRAINT `patient_email_validation` CHECK (`email` REGEXP "^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"),
    CONSTRAINT `patient_unique_user_id` UNIQUE (`user_id`),
    CONSTRAINT `patient_unique_user_slug` UNIQUE (`user_slug`),
    CONSTRAINT `patient_min_length_first_name` CHECK ( CHAR_LENGTH(`first_name`) > 0 ),
    CONSTRAINT `patientmin_length_last_name_` CHECK ( CHAR_LENGTH(`last_name`) > 0 ),
    CONSTRAINT `patient_bool_blood_pressure` CHECK ( `blood_pressure` REGEXP "^\d{1,3}/\d{1,3}$" )
) ENGINE = InnoDB;

-- CREATE INDEX "index_city_id" --------------------------------
CREATE INDEX `index_city_id` ON `patient`(`city_id`);

-- CREATE INDEX "index_country_id" -----------------------------
CREATE INDEX `index_country_id` ON `patient`(`country_id`);


-- Create Links ------------------------------------------------


-- CREATE LINK "speciality_specialist_link" --------------------
ALTER TABLE `specialist`
    ADD CONSTRAINT `speciality_specialist_link` FOREIGN KEY ( `speciality_id` )
    REFERENCES `speciality`( `id` )
    ON DELETE Cascade
    ON UPDATE Cascade;

-- CREATE LINK "user_patient_link" -----------------------------
ALTER TABLE `patient`
    ADD CONSTRAINT `user_patient_link` FOREIGN KEY ( `user_id` )
    REFERENCES `user`( `id` )
    ON DELETE Cascade
    ON UPDATE Cascade;


-- CREATE VIEWS ------------------------------------------------


-- CREATE VIEW "specialties_view" ------------------------------
CREATE OR REPLACE VIEW `specialties_view` AS (
    SELECT `id`, `name`, `image_url`
    FROM `speciality`
    WHERE `is_deleted` = 0
);

-- CREATE VIEW "user_view" -------------------------------------
CREATE OR REPLACE VIEW `user_view` AS (
    SELECT `id`, `login_password`, `email`, `is_verified`
    FROM `user`
    WHERE `is_deleted` = 0
);

-- CREATE VIEW "patient_view" ----------------------------------
CREATE OR REPLACE VIEW `patient_view` AS (
    SELECT 
        `id`,
        `email`,
        `first_name`,
        `last_name`,
        `weight`,
        `height`,
        `blood_pressure`,
        `blood_sugar`,
        `login_password`,
        `city_id`,
        `country_id`,
        `user_id`,
        `user_slug`
    FROM `patient`
    WHERE `is_deleted` = 0
);
