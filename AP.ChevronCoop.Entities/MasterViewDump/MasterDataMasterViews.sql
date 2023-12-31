/****** Object:  View [MasterData].[BankMasterView]    Script Date: 7/6/2023 5:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- MasterData.BankMasterView source

CREATE   VIEW [MasterData].[BankMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY Bank.[Id]) AS RowNumber,
Bank.[Id],
 Bank.[Code],
 Bank.[SortCode],
 Bank.[Name],
 Bank.[Address],
 Bank.[ContactName],
 Bank.[ContactDetails],
 Bank.[Description],
 Bank.[IsActive],
 Bank.[CreatedByUserId],
 Bank.[DateCreated],
 Bank.[UpdatedByUserId],
 Bank.[DateUpdated],
 Bank.[DeletedByUserId],
 Bank.[IsDeleted],
 Bank.[DateDeleted],
 Bank.[RowVersion],
 Bank.[FullText],
 Bank.[Tags],
 Bank.[Caption]

 from MasterData.[Bank] Bank
GO
/****** Object:  View [MasterData].[ChargeMasterView]    Script Date: 7/6/2023 5:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- MasterData.ChargeMasterView source

CREATE   VIEW [MasterData].[ChargeMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY Charge.[Id]) AS RowNumber,
Charge.[Id],
 Charge.[Code],
 Charge.[Name],
 Charge.[Method],
 Charge.[Target],
 Charge.[CalculationMethod],
 Charge.[CurrencyId],
 Charge.[ChargeValue],
 Charge.[MaximumCharge],
 Charge.[MinimimumCharge],
 Charge.[Description],
 Charge.[IsActive],
 Charge.[CreatedByUserId],
 Charge.[DateCreated],
 Charge.[UpdatedByUserId],
 Charge.[DateUpdated],
 Charge.[DeletedByUserId],
 Charge.[IsDeleted],
 Charge.[DateDeleted],
 Charge.[RowVersion],
 Charge.[FullText],
 Charge.[Tags],
 Charge.[Caption],
CurrencyId.[Code] as CurrencyId_Code,
CurrencyId.[Name] as CurrencyId_Name,
CurrencyId.[Symbol] as CurrencyId_Symbol,
CurrencyId.[IsoSymbol] as CurrencyId_IsoSymbol,
CurrencyId.[DecimalPlaces] as CurrencyId_DecimalPlaces,
CurrencyId.[Format] as CurrencyId_Format,
CurrencyId.[IsActive] as CurrencyId_IsActive,
CurrencyId.[CreatedByUserId] as CurrencyId_CreatedByUserId,
CurrencyId.[UpdatedByUserId] as CurrencyId_UpdatedByUserId,
CurrencyId.[DeletedByUserId] as CurrencyId_DeletedByUserId,
CurrencyId.[IsDeleted] as CurrencyId_IsDeleted,
CurrencyId.[Tags] as CurrencyId_Tags,
CurrencyId.[Caption] as CurrencyId_Caption

 from MasterData.[Charge] Charge

 left join MasterData.Currency CurrencyId on CurrencyId.Id=Charge.CurrencyId
GO
/****** Object:  View [MasterData].[CurrencyMasterView]    Script Date: 7/6/2023 5:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- MasterData.CurrencyMasterView source

CREATE   VIEW [MasterData].[CurrencyMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY Currency.[Id]) AS RowNumber,
Currency.[Id],
 Currency.[Code],
 Currency.[Name],
 Currency.[Symbol],
 Currency.[IsoSymbol],
 Currency.[DecimalPlaces],
 Currency.[Format],
 Currency.[Description],
 Currency.[IsActive],
 Currency.[CreatedByUserId],
 Currency.[DateCreated],
 Currency.[UpdatedByUserId],
 Currency.[DateUpdated],
 Currency.[DeletedByUserId],
 Currency.[IsDeleted],
 Currency.[DateDeleted],
 Currency.[RowVersion],
 Currency.[FullText],
 Currency.[Tags],
 Currency.[Caption]

 from MasterData.[Currency] Currency
GO
/****** Object:  View [MasterData].[DepartmentMasterView]    Script Date: 7/6/2023 5:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- MasterData.DepartmentMasterView source

CREATE   VIEW [MasterData].[DepartmentMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY Department.[Id]) AS RowNumber,
Department.[Id],
 Department.[Name],
 Department.[Description],
 Department.[IsActive],
 Department.[CreatedByUserId],
 Department.[DateCreated],
 Department.[UpdatedByUserId],
 Department.[DateUpdated],
 Department.[DeletedByUserId],
 Department.[IsDeleted],
 Department.[DateDeleted],
 Department.[RowVersion],
 Department.[FullText],
 Department.[Tags],
 Department.[Caption]

 from MasterData.[Department] Department
GO
/****** Object:  View [MasterData].[GlobalCodeMasterView]    Script Date: 7/6/2023 5:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- MasterData.GlobalCodeMasterView source

CREATE   VIEW [MasterData].[GlobalCodeMasterView]
  as select

 ROW_NUMBER() OVER (ORDER BY GlobalCode.[Id]) AS RowNumber,
GlobalCode.[Id],
 GlobalCode.[CodeType],
 GlobalCode.[Code],
 GlobalCode.[Name],
 GlobalCode.[SystemFlag],
 GlobalCode.[Description],
 GlobalCode.[IsActive],
 GlobalCode.[CreatedByUserId],
 GlobalCode.[DateCreated],
 GlobalCode.[UpdatedByUserId],
 GlobalCode.[DateUpdated],
 GlobalCode.[DeletedByUserId],
 GlobalCode.[IsDeleted],
 GlobalCode.[DateDeleted],
 GlobalCode.[RowVersion],
 GlobalCode.[FullText],
 GlobalCode.[Tags],
 GlobalCode.[Caption]

 from MasterData.[GlobalCode] GlobalCode
GO
/****** Object:  View [MasterData].[LocationMasterView]    Script Date: 7/6/2023 5:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- MasterData.LocationMasterView source

CREATE   VIEW [MasterData].[LocationMasterView]
  as


  with children(Id,ChildCount)
as (
SELECT    p.Id, Count(c.ParentID) as ChildCount
FROM      MasterData.Location  p
LEFT JOIN MasterData.Location  c ON p.Id = c.ParentId
GROUP BY  p.Id  )


select
 ROW_NUMBER() OVER (ORDER BY Location.[Id]) AS RowNumber,

  Location.[Id],
 Location.[LocationType],
 Location.[ParentId],
 Location.[Code],
 Location.[Name],
 Location.[SystemFlag],
 Location.[Description],
 Location.[IsActive],
 Location.[CreatedByUserId],
 Location.[DateCreated],
 Location.[UpdatedByUserId],
 Location.[DateUpdated],
 Location.[DeletedByUserId],
 Location.[IsDeleted],
 Location.[DateDeleted],
 Location.[RowVersion],
 Location.[FullText],
 Location.[Tags],
 Location.[Caption],
Header.[LocationType] as Header_LocationType,
Header.[ParentId] as Header_ParentId,
Header.[Code] as Header_Code,
Header.[Name] as Header_Name,
Header.[SystemFlag] as Header_SystemFlag,
Header.[IsActive] as Header_IsActive,
Header.[CreatedByUserId] as Header_CreatedByUserId,
Header.[UpdatedByUserId] as Header_UpdatedByUserId,
Header.[DeletedByUserId] as Header_DeletedByUserId,
Header.[IsDeleted] as Header_IsDeleted,
Header.[Tags] as Header_Tags,
Header.[Caption] as Header_Caption

,
subs.ChildCount,
 case
 when subs.ChildCount > 0 then cast(1 as bit)
        else cast(0 as bit)
        end as HasChildren

 from MasterData.[Location] Location
 left join children subs on Location.Id=subs.Id
 left join MasterData.Location Header on Header.Id=Location.ParentId
GO
