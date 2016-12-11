package com.hcmus.model.service;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.hcmus.model.pojo.Merchant;
import com.hcmus.model.pojo.MerchantSummaryDaily;
import com.hcmus.model.pojo.MerchantSummaryDailyTiny;
//@Service("MerchantService")
//extends HibernateDaoSupport
@Transactional
@Service
public class MerchantSummaryDailyService  {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	
	public List<MerchantSummaryDaily> GetReportDataGenerality(String startDate, String endDate){
		Session session = getSession();
		List<MerchantSummaryDaily> result = new ArrayList<MerchantSummaryDaily>();
		try{
			Query query = session.createSQLQuery("exec SP_GetReportData_Generality :startDate, :endDate").addEntity(MerchantSummaryDaily.class).setParameter("startDate", startDate).setParameter("endDate", endDate);
			result = (List<MerchantSummaryDaily>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
	
	public List<MerchantSummaryDaily> GetReportData()
    {
		Session session = getSession();
		List<MerchantSummaryDaily> result = new ArrayList<MerchantSummaryDaily>();
		try{
			Query query = session.createSQLQuery("exec SP_GetReportData_Default").addEntity(MerchantSummaryDaily.class);
			result = (List<MerchantSummaryDaily>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
    }
	
	public List<MerchantSummaryDaily> GetMerchantSummaryDaily()
	{
		Session session = getSession();
		List<MerchantSummaryDaily> result = new ArrayList<MerchantSummaryDaily>();
		try{
			Query query = session.createSQLQuery("SELECT TOP 100 * FROM MERCHANT_SUMMARY_DAILY").addEntity(MerchantSummaryDaily.class);
			result = (List<MerchantSummaryDaily>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
	
	public List<MerchantSummaryDaily> GetMerchantSummaryDaily(String Date)
	{
		Session session = getSession();
		List<MerchantSummaryDaily> result = new ArrayList<MerchantSummaryDaily>();
		try{
			Query query = session.createSQLQuery("SELECT TOP 100 * from MERCHANT_SUMMARY_DAILY WHERE REPORTDATE = '" + Date + "'").addEntity(MerchantSummaryDaily.class);
			result = (List<MerchantSummaryDaily>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
	
	private boolean MERCHANT_SUMMARY_DAILYExists(String id)
    {
		Session session = getSession();
		try {
			session.beginTransaction();
			Query query = session.createSQLQuery("SELECT 1 FROM MERCHANT_SUMMARY_DAILY WHERE reportDate = '" + id + "'").addEntity(MerchantSummaryDaily.class);
			return (query.uniqueResult() != null);
		} catch(HibernateException e){
			session.getTransaction().rollback();
			return false;
		} finally{
			session.close();
		}
    }
	
	public boolean PostMERCHANT_SUMMARY_DAILY(MerchantSummaryDaily merchantSummaryDaily){//insert
		Session session = getSession();
		try{
			session.beginTransaction();
			session.save(merchantSummaryDaily);
			session.getTransaction().commit();
			return true;
		}catch(HibernateException e){
			session.beginTransaction().rollback();
			return false;
		}
	}
	
	public boolean DeleteMERCHANT_SUMMARY_DAILY(String id) {
		Session session = getSession();
		
		try{
			session.beginTransaction();
			Query query = session.createSQLQuery("DELETE FROM MERCHANT_SUMMARY_DAILY WHERE REPORTDATE = '" + id + "'");
			query.executeUpdate();
			return true;
		}catch(HibernateException e){
			session.beginTransaction().rollback();
			return false;
		}
	}
	
	public boolean PutMERCHANT_SUMMARY_DAILY(String id, MerchantSummaryDaily mERCHANT_SUMMARY_DAILY){//update
		Session session = getSession();
		try{
			session.beginTransaction();
			boolean check = this.MERCHANT_SUMMARY_DAILYExists(id);
			if (check == true)
				session.update(mERCHANT_SUMMARY_DAILY);
			session.getTransaction().commit();
			return true;
		}catch(HibernateException e){
			session.getTransaction().rollback();
			return false;
		}
	}
}