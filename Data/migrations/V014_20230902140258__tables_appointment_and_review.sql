-- Target Database ---------------------------------------------
USE `docconnect`;


-- CREATE TABLE "review" ---------------------------------------
CREATE TABLE IF NOT EXISTS `review`(
    `id` Int AUTO_INCREMENT NOT NULL,
    `patient_id` Int UNSIGNED NOT NULL,
    `doctor_id` Int UNSIGNED NOT NULL,
    `raiting` TinyInt(1) NOT NULL DEFAULT 5,
    `content` Text NOT NULL,
    `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY (`id`),
    CONSTRAINT `unique_id` UNIQUE (`id`)
) ENGINE = InnoDB;

-- CREATE INDEX "index_doctor_id" ------------------------------
CREATE INDEX `index_doctor_id` ON `review`(`doctor_id`);

-- CREATE INDEX "index_patient_id" -----------------------------
CREATE INDEX `index_patient_id` ON `review`(`patient_id`);


-- CREATE TABLE "appointment" ----------------------------------
CREATE TABLE IF NOT EXISTS `appointment`(
    `id` BIGINT AUTO_INCREMENT NOT NULL,
    `doctor_id` Int UNSIGNED NOT NULL,
    `patient_id` Int UNSIGNED NOT NULL,
    `time_slot` DateTime NOT NULL,
    `is_canceled` Bool NOT NULL DEFAULT False,
    `notes` Text NOT NULL,
    `created_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` Timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `is_deleted` Bool NOT NULL DEFAULT False,
    PRIMARY KEY (`id`, `patient_id`, `time_slot`),
    CONSTRAINT `unique_id` UNIQUE (`id`),
    CONSTRAINT `unique_patient_timeslot` UNIQUE (`patient_id`, `time_slot`)
) ENGINE = InnoDB;

-- CREATE INDEX "index_patient_id" -----------------------------
CREATE INDEX `index_patient_id` ON `appointment`(`patient_id`);

-- CREATE INDEX "index_time_slot" ------------------------------
CREATE INDEX `index_time_slot` ON `appointment`(`time_slot`);

-- CREATE INDEX "index_doctor_id" ------------------------------
CREATE INDEX `index_doctor_id` ON `appointment`(`doctor_id`);


-- CREATE LINK "doctor_review_link" ----------------------------
ALTER TABLE `review`
    ADD CONSTRAINT `doctor_review_link` FOREIGN KEY ( `doctor_id` )
    REFERENCES `doctor`( `id` )
    ON DELETE No Action
    ON UPDATE Cascade;
    
-- CREATE LINK "patient_review_link" ---------------------------
ALTER TABLE `review`
    ADD CONSTRAINT `patient_review_link` FOREIGN KEY ( `patient_id` )
    REFERENCES `patient`( `id` )
    ON DELETE No Action
    ON UPDATE Cascade;

-- DROP LINK "user_patient_link" -------------------------------
ALTER TABLE `patient` DROP FOREIGN KEY `user_patient_link`;

-- CREATE LINK "user_patient_link" -----------------------------
ALTER TABLE `patient`
	ADD CONSTRAINT `user_patient_link` FOREIGN KEY ( `user_id` )
	REFERENCES `user`( `id` )
	ON DELETE No Action
	ON UPDATE Cascade;

-- CREATE LINK "doctor_appointment_link" -----------------------
ALTER TABLE `appointment`
    ADD CONSTRAINT `doctor_appointment_link` FOREIGN KEY ( `doctor_id` )
    REFERENCES `doctor`( `id` )
    ON DELETE No Action
    ON UPDATE Cascade;
    
-- CREATE LINK "patient_appointment_link" ----------------------
ALTER TABLE `appointment`
    ADD CONSTRAINT `patient_appointment_link` FOREIGN KEY ( `patient_id` )
    REFERENCES `patient`( `id` )
    ON DELETE No Action
    ON UPDATE Cascade;
