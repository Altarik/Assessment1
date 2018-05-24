# Assessment1

- This website manages Flights.
- Data is stored in a .mdf file (in "app_data" folder), feel free to explore it (to get Aircraft data, used in computations)
- GPS Distance is computed using DBGeography column types 
- Data is managed by Entity Framework (DB first) (I invite you to explore the Entity schema).
- The computation of Consumed Fuel and the distance have been done on a Business class (it is also possible to compute directly on the Database using stocked procedures and/or computed columns).
- Once a Flight is created, the Consumed Fuel and the distance are computed then displayed directly on the index/details screens.
