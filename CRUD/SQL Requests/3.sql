--Суммарное значение Volume и MaxVolume резервуаров по каждому заводу
select min(F.Name) as 'Factory name', min(F.Description) as 'Factory description', SUM(T.Volume) as Volume, 
SUM(T.MaxVolume) as MaxVolume from Factory F
inner join Unit U on U.FactoryId = F.Id
inner join Tank T on T.UnitId = U.Id
group by F.Id