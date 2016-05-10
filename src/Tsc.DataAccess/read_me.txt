=======================
mongodb-rest használata
=======================

 - előfeltétele: MongoDB telepítve!
   - https://www.mongodb.com/download-center?jmp=nav#community
   - set up db path: ex.: mongod --dbpath "d:\Program Files\MogoDB\Data"
 - NodeJS install : 4.4.3
 - proxy beállítása    npm config set proxy http://proxy-atc.atlanta.hp.com:8080 
   törlés: npm config rm proxy
 - könyvtár létrehozása (pl: c:\mongodb-rest)
 - a könyvtárban: npm install mongodb-rest 
 - server indítása c:\mongodb-rest\node_modules\.bin\mongodb-rest.cmd
 - lekérdezés: http://localhost:3000/dbname/collectionname 
          pl.: http://localhost:3000/hpedemo/teszt
 - leírás: https://github.com/tdegrunt/mongodb-rest
 