--SELECT SUM (Tanks.Volume) AS "Volume", SUM(Tanks.MaxVolume) AS "MaxVolume", 
--COUNT(Tanks.ID) AS "Count units" FROM Tanks
select Unit.Name, Tank.Name, Tank.Volume from Unit
inner join Tank on Tank.UnitId = Unit.Id
where Tank.Volume > 1000