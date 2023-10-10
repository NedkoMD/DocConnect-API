-- Target Database ---------------------------------------------
USE `docconnect`;

-- CREATE FIELD "id" -------------------------------------------
ALTER TABLE `specialist` 
	ADD COLUMN `id` Int UNSIGNED AUTO_INCREMENT NOT NULL,
    DROP PRIMARY KEY,
	ADD PRIMARY KEY( `id` );
-- -------------------------------------------------------------
