-- Target Database ---------------------------------------------
USE `docconnect`;

-- CHANGE "PRIMARY KEY" OF "TABLE "appointment" ----------------
ALTER TABLE `appointment` DROP PRIMARY KEY;
ALTER TABLE `appointment` ADD PRIMARY KEY( `id` );

-- DROP INDEX "unique_patient_timeslot" ------------------------
DROP INDEX `unique_patient_timeslot` ON `appointment`;
-- -------------------------------------------------------------
