﻿CREATE TABLE URL_STORE (
		ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		URL varchar(500) NOT NULL,
		SMALL_URL varchar(50)
)