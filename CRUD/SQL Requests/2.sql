--Суммарное значение Volume и MaxVolume, а также количество резервуаров по каждой установке,
--с выводом имени установки, а также имени и описания завода, к которому относится установка

select min(U.Name) as 'Unit name', SUM(T.Volume) as Volume, SUM(T.MaxVolume) as MaxVolume,
COUNT(T.Id) as 'Count Tanks', min(F.Name), min(F.Description)
from Unit U
inner join Tank T on T.UnitId = U.Id
inner join Factory F on U.FactoryId = F.Id
GROUP BY U.Id 