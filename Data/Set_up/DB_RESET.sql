-- RESET SCRIPT / DO NOT EXECUTE IN PRODUCTION !!

-- Drops the whole schema
DROP DATABASE IF EXISTS `docconnect`;

-- Drops the created Roles
DROP ROLE IF EXISTS `docconnect_admin`, `docconnect_reader`, `docconnect_contributor`;

-- Drops the created users
DROP USER IF EXISTS 'dev'@'%', 'analytics'@'%', 'backend'@'%';
