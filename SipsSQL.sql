DROP TABLE IF EXISTS AddIn_OrderDetail;
DROP TABLE IF EXISTS AddIn;
DROP TABLE IF EXISTS OrderDetail;
DROP TABLE IF EXISTS Item;
DROP TABLE IF EXISTS ItemSize;
DROP TABLE IF EXISTS [Transaction];
DROP TABLE IF EXISTS Rating;
DROP TABLE IF EXISTS Contact;
DROP TABLE IF EXISTS [Store];
DROP TABLE IF EXISTS Credential;


CREATE TABLE Credential ( pkUserTypeID INTEGER CONSTRAINT Credential_PK PRIMARY KEY AUTOINCREMENT
							, userType			INTEGER		 	NOT NULL);
							
					
CREATE TABLE [Store] ( pkStoreID INTEGER CONSTRAINT Store_PK PRIMARY KEY AUTOINCREMENT
						, storeHours			VARCHAR(255)	NOT NULL);
						

CREATE TABLE Contact ( pkUserID INTEGER CONSTRAINT Contact_PK PRIMARY KEY AUTOINCREMENT
						, firstName				VARCHAR(30) 	NOT NULL
						, lastName				VARCHAR(30)
						, phoneNumber			VARCHAR(20) 	NOT NULL
						, email					VARCHAR(50) 	NOT NULL
						, unit					INTEGER		 	NOT NULL
						, street				VARCHAR(50) 	NOT NULL
						, city					VARCHAR(50) 	NOT NULL
						, province				VARCHAR(20) 	NOT NULL
						, postalCode			VARCHAR(10) 	NOT NULL
						, birthDate     		DATETIME		
						, isDrinkRedeemed		VARCHAR(10) 	NOT NULL
						, fkUserTypeID			INTEGER			NOT NULL
						, fkStoreID				INTEGER			NOT NULL
						, CONSTRAINT ContactCredentialFK FOREIGN KEY (fkUserTypeID) REFERENCES Credential(pkUserTypeID) ON UPDATE CASCADE ON DELETE CASCADE
						, CONSTRAINT ContactStoreFK FOREIGN KEY (fkStoreID) REFERENCES [Store](pkStoreID) ON UPDATE CASCADE ON DELETE CASCADE
						, CONSTRAINT CustomerContact UNIQUE (email));
						
						
CREATE TABLE Rating ( pkRatingID INTEGER CONSTRAINT Rating_PK PRIMARY KEY AUTOINCREMENT
						, rating				VARCHAR(5) 		NOT NULL
						, [date]				DATETIME		NOT NULL
						, comment				VARCHAR(255)	NOT NULL
						, fkStoreID				INTEGER			NOT NULL
						, fkUserID				INTEGER			NOT NULL
						, CONSTRAINT RatingStoreFK FOREIGN KEY (fkStoreID) REFERENCES [Store](pkStoreID) ON UPDATE CASCADE ON DELETE CASCADE
						, CONSTRAINT RatingContactFK FOREIGN KEY (fkUserID) REFERENCES Contact(pkUserID) ON UPDATE CASCADE ON DELETE CASCADE);
						
						
CREATE TABLE [Transaction] ( pkTransactionID INTEGER CONSTRAINT Transaction_PK PRIMARY KEY AUTOINCREMENT
								, totalPrice 	DECIMAL(10, 2)	NOT NULL
								, timeOrdered	VARCHAR(30)		NOT NULL
								, dateOrdered	DATETIME 		NOT NULL
								, totalItems	INTEGER			NOT NULL
								, isOrdered		VARCHAR(10)		NOT NULL
								, isPickedUp	VARCHAR(10)		NOT NULL
								, fkStoreID		INTEGER			NOT NULL
								, CONSTRAINT TransactionStoreFK FOREIGN KEY (fkStoreID) REFERENCES [Store](pkStoreID) ON UPDATE CASCADE ON DELETE CASCADE);
								
		
CREATE TABLE Item ( pkItemID INTEGER CONSTRAINT Item_PK PRIMARY KEY AUTOINCREMENT
						, name					VARCHAR(255)	NOT NULL
						, image					BLOB			NOT NULL
						, description			VARCHAR(255)	NOT NULL
						, ice					VARCHAR(10)		NOT NULL
						, sweetness				VARCHAR(10)		NOT NULL
						, basePrice				DECIMAL(10, 2)	NOT NULL
						, isBirthdayDrink		VARCHAR(10)		NOT NULL
						, CONSTRAINT UniqueName UNIQUE (name));
						
						
CREATE TABLE ItemSize ( pkSizeID INTEGER CONSTRAINT Size_PK PRIMARY KEY AUTOINCREMENT
						, sizeName				VARCHAR(30)		NOT NULL
						, priceModifier			DECIMAL(10, 2)	NOT NULL);
						
						
CREATE TABLE AddIn ( pkAddInID INTEGER CONSTRAINT AddIn_PK PRIMARY KEY AUTOINCREMENT
						, addInName				VARCHAR(30)		NOT NULL
						, priceModifier			DECIMAL(10, 2)	NOT NULL);
				
				
CREATE TABLE OrderDetail ( pkOrderDetailID INTEGER CONSTRAINT OrderDetail_PK PRIMARY KEY AUTOINCREMENT
							, price				DECIMAL(10, 2) 	NOT NULL
							, quantity			INTEGER			NOT NULL
							, totalPrice		DECIMAL(10, 2)	NOT NULL
							, fkItemID			INTEGER			NOT NULL
							, fkTransactionID	INTEGER			NOT NULL
							, fkSizeID			INTEGER			NOT NULL
							, CONSTRAINT OrderDetailItemFK FOREIGN KEY (fkItemID) REFERENCES Item(pkItemID) ON UPDATE CASCADE ON DELETE CASCADE
							, CONSTRAINT OrderDetailTransactionFK FOREIGN KEY (fkTransactionID) REFERENCES [Transaction](pkTransactionID) ON UPDATE CASCADE ON DELETE CASCADE
							, CONSTRAINT OrderDetailItemSizeFK FOREIGN KEY (fkSizeID) REFERENCES ItemSize(pkSizeID) ON UPDATE CASCADE ON DELETE CASCADE);
							
							
CREATE TABLE AddIn_OrderDetail ( fkAddInID 		INTEGER			NOT NULL
							, fkOrderDetailID	INTEGER			NOT NULL
							, quantity			INTEGER			NOT NULL
							, CONSTRAINT FK_AddIn FOREIGN KEY (fkAddInID) REFERENCES AddIn(pkAddInID) ON UPDATE CASCADE ON DELETE CASCADE
							, CONSTRAINT FK_OrderDetail FOREIGN KEY (fkOrderDetailID) REFERENCES OrderDetail(pkOrderDetailID) ON UPDATE CASCADE ON DELETE CASCADE
							, CONSTRAINT PK_AddIn_OrderDetail PRIMARY KEY (fkAddInID, fkOrderDetailID));