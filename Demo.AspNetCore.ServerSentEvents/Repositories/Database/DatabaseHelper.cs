using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinancialCharts.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinancialCharts.Repositories.Database
{
	internal static  class DatabaseHelper
	{
		internal static Dictionary<string, string> GetConnStringAndProvider()
		{
			var connString = (string)ConfigHelper.GetConfigValue("DatabaseConnectionString");
			var provider = (string)ConfigHelper.GetConfigValue("DatabaseProviderName");

			var dict = new Dictionary<string, string>();
			dict.Add("connString",connString);
			dict.Add("provider", provider);

			return dict;
		}

		internal static object GetDatabaseEntities(string connString, Entity entityName, string queryPredicate = "")
		{
			object res = null;
			string query = "";

			List<Asset> assetList = null;
			List<DataSeries> seriesList = null;
			List<ExpirationDate> datesList = null;
			List<decimal> strikeList = null;
			List<decimal> volatList = null;

			switch (entityName)
			{
				case Entity.Asset:
					query = "select Id, Name " +
							"from dbo.Asset ;";
					assetList = new List<Asset>();
					break;
				case Entity.DataSeries:
					query = @"select distinct V.AssetId, V.ExpirationDateId, V.IndividualSeriesId, 
							I.Name
						from IndividualSeries as I
						inner join VolatilityStrike as V
							on I.Id = V.IndividualSeriesId";
					seriesList = new List<DataSeries>();
					break;
				case Entity.Strike:
					query = @"select Strike
					  from VolatilityStrike
					  " + queryPredicate + @"
					  order by Strike";
					strikeList = new List<decimal>();
					break;
				case Entity.Volatility:
					query = @"select Volatility
					  from VolatilityStrike
					  " + queryPredicate + @"
					  order by Strike";
					volatList = new List<decimal>();
					break;
				case Entity.ExpirationDate:
					query = @"select Id, Date, AssetId from dbo.ExpirationDate";
					datesList = new List<ExpirationDate>();
					break;
			}
			
			using (var dbConnection = new SqlConnection(connString))
			{
				try
				{
					dbConnection.Open();
					
					using (var command = new SqlCommand(query, dbConnection))
					{

						using (var dataReader = command.ExecuteReader())
						{
							if (dataReader.HasRows)
							{
								while (dataReader.Read())
								{
									switch (entityName)
									{
										case Entity.Asset:
											int id = dataReader.GetInt32(0);
											string name = dataReader.GetValue(1).ToString();
											var asset = new Asset() { Id = id, Name = name };
											assetList.Add(asset);
											break;
										case Entity.DataSeries:
											int seriesAssetId = dataReader.GetInt32(0);
											int seriesdateId = dataReader.GetInt32(1);
											int seriesId = dataReader.GetInt32(2);
											string seriesName = dataReader.GetValue(3).ToString();
											var dataSeries = new DataSeries(seriesId, seriesAssetId, seriesdateId, seriesName);
											seriesList.Add(dataSeries);
											break;
										case Entity.Strike:
											decimal strike = dataReader.GetDecimal(0);
											strikeList.Add(strike);
											break;
										case Entity.Volatility:
											decimal vol = dataReader.GetDecimal(0);
											volatList.Add(vol);
											break;
										case Entity.ExpirationDate:
											int dateId = dataReader.GetInt32(0);
											DateTime date = DateTime.Parse(dataReader.GetValue(1).ToString());
											int assetId = dataReader.GetInt32(2);
											var expDate = new ExpirationDate()
											{
												Id = dateId,
												ExpDate = date,
												AssetId = assetId
											};
											datesList.Add(expDate);
											break;
									}
									
								}
							}
						}
					}
				}
				catch
				{
				}
			}

			switch (entityName)
			{
				case Entity.Asset:
					res = assetList;
					break;
				case Entity.DataSeries:
					res = seriesList;
					break;
				case Entity.Strike:
					res = strikeList;
					break;
				case Entity.Volatility:
					res = volatList;
					break;
				case Entity.ExpirationDate:
					res = datesList;
					break;
			}

			return res;
		}
	}
}
