use blg;
-- CREATE TABLE profiles
-- (
--     id VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     name VARCHAR(255),
--     picture VARCHAR(255),
--     PRIMARY KEY (id)
-- );

-- CREATE TABLE blgs
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     title VARCHAR(255) NOT NULL,
--     body VARCHAR(255) Not NULL,
--     isPublished TINYINT NOT NULL DEFAULT 0,
--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- );

CREATE TABLE cmnts
(
   id INT NOT NULL AUTO_INCREMENT,
   creatorId VARCHAR(255) NOT NULL,
   body VARCHAR(255) Not NULL,
   
)