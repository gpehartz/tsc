=======================
mongodb-rest használata
=======================

 - NodeJS install : 4.4.3
 - proxy beállítása    npm config set proxy http://proxy-atc.atlanta.hp.com:8080 
   törlés: npm config rm proxy
 - könyvtár létrehozása (pl: c:\mongodb-rest)
 - a könyvtárban: npm install mongodb-rest 
 - server indítása c:\mongodb-rest\node_modules\.bin\mongodb-rest.cmd
 - lekérdezés: http://localhost:3000/dbname/collectionname 
          pl.: http://localhost:3000/hpedemo/teszt
 - leírás: https://github.com/tdegrunt/mongodb-rest
 