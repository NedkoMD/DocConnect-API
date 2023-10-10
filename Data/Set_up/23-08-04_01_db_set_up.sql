-- Create the Schema -------------------------------------------
CREATE DATABASE IF NOT EXISTS `docconnect`;
USE `docconnect`;

-- Create our database roles -----------------------------------
CREATE ROLE 'docconnect_admin', 'docconnect_reader', 'docconnect_contributor';

-- Create Admin, Read & Write Roles ----------------------------
GRANT ALL ON `docconnect`.* TO 'docconnect_admin';
GRANT SELECT ON `docconnect`.* TO 'docconnect_reader';
GRANT INSERT, UPDATE ON `docconnect`.* TO 'docconnect_contributor'; -- Note: NO delete permition

-- Create Users for the sprint database ------------------------
CREATE USER 'dev'@'%' IDENTIFIED BY 'dev_pass';
CREATE USER 'analytics'@'%' IDENTIFIED BY 'analytics_pass';
CREATE USER 'backend'@'%' IDENTIFIED BY 'backend_pass'; -- BACKEND_GREEN_PASS

-- GRANT ROLES to Users ----------------------------------------
GRANT 'docconnect_admin' TO 'dev'@'%';
GRANT 'docconnect_reader' TO 'analytics'@'%';
GRANT 'docconnect_reader', 'docconnect_contributor' TO 'backend'@'%';

-- SET DEFAULT ROLES for Each User -----------------------------
SET DEFAULT ROLE 'docconnect_admin' TO 'dev'@'%';
SET DEFAULT ROLE 'docconnect_reader' TO 'analytics'@'%';
SET DEFAULT ROLE 'docconnect_reader', 'docconnect_contributor' TO 'backend'@'%';

-- Allow Users to Create Deterministic Functions ---------------
SET global log_bin_trust_function_creators = 1;

-- Refresh privileges ------------------------------------------
FLUSH PRIVILEGES;

-- Refference Material
-- https://dev.mysql.com/doc/refman/8.0/en/create-database.html
-- https://dev.mysql.com/doc/refman/8.0/en/create-role.html
-- https://dev.mysql.com/doc/refman/8.0/en/create-user.html
-- https://dev.mysql.com/doc/refman/8.0/en/set-default-role.html