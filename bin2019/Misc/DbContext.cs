using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JEast.Misc
{
	public class DbContext<T> where T : class, new()
	{
		public DbContext()
		{
			Db = new SqlSugarClient(new ConnectionConfig()
			{
				//ConnectionString = "Password=bestunion;User ID=JEast;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.80)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)))",
				ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString,
				DbType = SqlSugar.DbType.Oracle,
				IsAutoCloseConnection = true,
				InitKeyType = InitKeyType.Attribute
			});

			//调式代码 用来打印SQL
			Db.Aop.OnLogExecuting = (sql, pars) =>
			{
				Console.WriteLine(sql + "\r\n" +
					Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
				Console.WriteLine();
			};
		}
		public SqlSugarClient Db;
		public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作

		/// <summary>
		/// 获取所有
		/// </summary>
		/// <returns></returns>
		public virtual List<T> GetList()
		{
			return CurrentDb.GetList();
		}



		/// <summary>
		/// 删除对象
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public virtual bool Delete(T obj)
		{
			return CurrentDb.Delete(obj);
		}

		/// <summary>
		/// 根据Id删除
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual bool DeleteById(dynamic id)
		{
			return CurrentDb.DeleteById(id);
		}

		/// <summary>
		/// 根据Id数组删除
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public virtual bool DeleteByIds(dynamic[] ids)
		{
			return CurrentDb.DeleteByIds(ids);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		/// <param name="whereExpression"></param>
		/// <returns></returns>
		public virtual bool Delete(Expression<Func<T, bool>> whereExpression)
		{
			return CurrentDb.Delete(whereExpression);
		}


		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual bool Update(T obj)
		{
			return CurrentDb.Update(obj);
		}

		/// <summary>
		/// 更新多条记录  
		/// </summary>
		/// <param name="objs"></param>
		/// <returns></returns>
		public virtual bool UpdateRange(T[] objs)
		{
			return CurrentDb.UpdateRange(objs);
		}

		/// <summary>
		/// 更新多条记录
		/// </summary>
		/// <param name="updateObjs"></param>
		/// <returns></returns>
		public virtual bool UpdateRange(List<T> updateObjs)
		{
			return CurrentDb.UpdateRange(updateObjs);
		}


		/// <summary>
		/// 根据Id返回单一记录
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual T GetById(dynamic id)
		{
			return CurrentDb.GetById(id);
		}

		/// <summary>
		/// 根据条件返回记录
		/// </summary>
		/// <param name="whereExpression"></param>
		/// <returns></returns>
		public virtual List<T> GetList(Expression<Func<T, bool>> whereExpression)
		{
			return CurrentDb.GetList(whereExpression);
		}

		/// <summary>
		/// 根据条件返回一条记录
		/// </summary>
		/// <param name="whereExpression"></param>
		/// <returns></returns>
		public virtual T GetSingle(Expression<Func<T, bool>> whereExpression)
		{
			return CurrentDb.GetSingle(whereExpression);
		}

		public virtual int Count(Expression<Func<T, bool>> whereExpression)
		{
			return CurrentDb.Count(whereExpression);
		}

		/// <summary>
		/// 插入一条记录
		/// </summary>
		/// <param name="insertObj"></param>
		/// <returns></returns>
		public virtual bool Insert(T insertObj)
		{
			return CurrentDb.Insert(insertObj);
		}

		/// <summary>
		/// 插入多条记录
		/// </summary>
		/// <param name="insertObjs"></param>
		/// <returns></returns>
		public virtual bool InsertRange(T[] insertObjs)
		{
			return CurrentDb.InsertRange(insertObjs);
		}

		/// <summary>
		/// 插入多条记录
		/// </summary>
		/// <param name="insertObjs"></param>
		/// <returns></returns>
		public virtual bool InsertRange(List<T> insertObjs)
		{
			return CurrentDb.InsertRange(insertObjs);
 
		}


	}
}
