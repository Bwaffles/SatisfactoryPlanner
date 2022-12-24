INSERT INTO resources.nodes (id, purity, biome, map_position_x, map_position_y, map_position_z, instance_name, resource_id) 
SELECT gen_random_uuid (), 'Impure', 'Titan Forest', 103317.648, 67498.78, 9540.116, 'Persistent_Level:PersistentLevel.BP_ResourceNode635', resource.id 
  FROM resources.resources AS resource  WHERE resource.code = 'Desc_OreBauxite_C';
INSERT INTO resources.nodes (id, purity, biome, map_position_x, map_position_y, map_position_z, instance_name, resource_id) 
SELECT gen_random_uuid (), 'Impure', 'Titan Forest', 89592.65, 62638.78, 10375.1162, 'Persistent_Level:PersistentLevel.BP_ResourceNode636', resource.id 
  FROM resources.resources AS resource  WHERE resource.code = 'Desc_OreBauxite_C';
INSERT INTO resources.nodes (id, purity, biome, map_position_x, map_position_y, map_position_z, instance_name, resource_id) 
SELECT gen_random_uuid (), 'Normal', 'Titan Forest', 106047.648, 50478.7852, 9765.116, 'Persistent_Level:PersistentLevel.BP_ResourceNode633', resource.id 
  FROM resources.resources AS resource  WHERE resource.code = 'Desc_OreBauxite_C';
INSERT INTO resources.nodes (id, purity, biome, map_position_x, map_position_y, map_position_z, instance_name, resource_id) 
SELECT gen_random_uuid (), 'Pure', 'Titan Forest', 116877.539, 51510.9063, 17359.5059, 'Persistent_Level:PersistentLevel.BP_ResourceNode634', resource.id 
  FROM resources.resources AS resource  WHERE resource.code = 'Desc_OreBauxite_C';
INSERT INTO resources.nodes (id, purity, biome, map_position_x, map_position_y, map_position_z, instance_name, resource_id) 
SELECT gen_random_uuid (), 'Impure', 'Rocky Desert', -132772.219, -187579.234, 46528.31, 'Persistent_Level:PersistentLevel.BP_ResourceNode632', resource.id 
  FROM resources.resources AS resource  WHERE resource.code = 'Desc_OreUranium_C';
DELETE FROM resources.nodes WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode629';
DELETE FROM resources.nodes WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode630';
DELETE FROM resources.nodes WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode627';
DELETE FROM resources.nodes WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode628';
DELETE FROM resources.nodes WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode626';
UPDATE resources.nodes 
   SET map_position_x = 26597.5039     , map_position_y = -193983.953     , map_position_z = -1683.89941
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode14_609';
UPDATE resources.nodes 
   SET map_position_x = -32599.6465     , map_position_y = -192537.5     , map_position_z = 2280.044
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode15';
UPDATE resources.nodes 
   SET map_position_x = -43612.14     , map_position_y = -190316.359     , map_position_z = 2291.03784
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode16';
UPDATE resources.nodes 
   SET map_position_x = 57144.1172     , map_position_y = -191300.438     , map_position_z = -1744.92786
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode23_96';
UPDATE resources.nodes 
   SET map_position_x = 15357.45     , map_position_y = -197672.219     , map_position_z = -1606.49817
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode24_97';
UPDATE resources.nodes 
   SET map_position_x = 29991.334     , map_position_y = -186849.656     , map_position_z = -1689.25549
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode25_98';
UPDATE resources.nodes 
   SET map_position_x = 44495.81     , map_position_y = -228950.031     , map_position_z = -1743.504
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode26_99';
UPDATE resources.nodes 
   SET map_position_x = 12162.998     , map_position_y = -225348.3     , map_position_z = -1744.18445
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode27_100';
UPDATE resources.nodes 
   SET map_position_x = -35725.8242     , map_position_y = -209521.1     , map_position_z = -1708.58337
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode28_101';
UPDATE resources.nodes 
   SET map_position_x = 52471.01     , map_position_y = -201464.719     , map_position_z = -1658.74146
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode29_102';
UPDATE resources.nodes 
   SET map_position_x = 148477.578     , map_position_y = -193869.422     , map_position_z = -1603.09473
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode30_103';
UPDATE resources.nodes 
   SET map_position_x = 147769.359     , map_position_y = -212912.344     , map_position_z = -1429.33936
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode31_104';
UPDATE resources.nodes 
   SET map_position_x = 146451.922     , map_position_y = -222638.25     , map_position_z = -1580.83691
 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode32_105';
