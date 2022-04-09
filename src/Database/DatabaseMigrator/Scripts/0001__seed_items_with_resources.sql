INSERT INTO factories.items (type, code, name, description, stack_size, can_be_deleted, energy_value, radioactive_decay, form, resource_sink_points) 
VALUES 
('Resource', 'Desc_OreIron_C', 'Iron Ore', 'Used for crafting.\r\nThe most essential basic resource.', 100, 'True', NULL, NULL, 'Solid', 1),
('Resource', 'Desc_Stone_C', 'Limestone', 'Used for crafting.\r\nBasic resource mainly used for stable foundations.', 100, 'True', NULL, NULL, 'Solid', 2),
('Resource', 'Desc_OreCopper_C', 'Copper Ore', 'Used for crafting.\r\nBasic resource mainly used for electricity.', 100, 'True', NULL, NULL, 'Solid', 3),
('Resource', 'Desc_Coal_C', 'Coal', 'Mainly used as fuel for vehicles & coal generators and for steel production.', 100, 'True', 300.000000, NULL, 'Solid', 3),
('Resource', 'Desc_Water_C', 'Water', 'It''s water.', NULL, 'True', NULL, NULL, 'Liquid', 5),
('Resource', 'Desc_OreGold_C', 'Caterium Ore', 'Caterium Ore is smelted into Caterium Ingots. Caterium Ingots are mostly used for advanced electronics.', 100, 'True', NULL, NULL, 'Solid', 7),
('Resource', 'Desc_OreBauxite_C', 'Bauxite', 'Bauxite is used to produce Alumina, which can be further refined into the Aluminum Scrap required to produce Aluminum Ingots.', 100, 'True', NULL, NULL, 'Solid', 8),
('Resource', 'Desc_NitrogenGas_C', 'Nitrogen Gas', 'Nitrogen can be used in a variety of ways, such as metallurgy, cooling, and Nitric Acid production. On Massage-2(AB)b, it can be extracted from underground gas wells.', NULL, 'True', NULL, NULL, 'Gas', 10),
('Resource', 'Desc_Sulfur_C', 'Sulfur', 'Sulfur is primarily used for Black Powder.', 100, 'True', NULL, NULL, 'Solid', 11),
('Resource', 'Desc_RawQuartz_C', 'Raw Quartz', 'Raw Quartz can be processed into Quartz Crystals and Silica, which both offer a variety of applications.', 100, 'True', NULL, NULL, 'Solid', 15),
('Resource', 'Desc_LiquidOil_C', 'Crude Oil', 'Crude Oil is refined into all kinds of Oil-based resources, like Fuel and Plastic.', NULL, 'True', 0.320000, NULL, 'Liquid', 30),
('Resource', 'Desc_OreUranium_C', 'Uranium', 'Uranium is a radioactive element. \r\nUsed to produce Encased Uranium Cells for Uranium Fuel Rods.\r\n\r\nCaution: Moderately Radioactive.', 100, 'True', NULL, 15.000000, 'Solid', 35);