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

import com.hcmus.model.pojo.MerchantSummaryDailyTiny;
import com.hcmus.model.pojo.MerchantSummaryDaily;
//@Service("MerchantService")
//extends HibernateDaoSupport
@Transactional
@Service
public class MerchantSummaryDailyTinyService  {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	
	public List<MerchantSummaryDailyTiny> GetMerchantSummaryDefault()
    {
		Session session = getSession();
		List<MerchantSummaryDailyTiny> result = new ArrayList<MerchantSummaryDailyTiny>();
		try{
			Query query = session.createSQLQuery("exec SP_GetMerchantSummary_Default").addEntity(MerchantSummaryDailyTiny.class);
			result =(List<MerchantSummaryDailyTiny>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
    }
	
	public List<MerchantSummaryDailyTiny> FindMerchantSummaryElement(String searchString)
    {
		Session session = getSession();
		List<MerchantSummaryDailyTiny> result = new ArrayList<MerchantSummaryDailyTiny>();
		try{
			Query query = session.createSQLQuery("exec SP_FindMerchantSummaryElement :Element").addEntity(MerchantSummaryDailyTiny.class).setParameter("Element", searchString);
			result =(List<MerchantSummaryDailyTiny>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
    }
	
	public List<MerchantSummaryDailyTiny> GetMerchantSummaryForAgentDefault(String agentCode)
    {
		Session session = getSession();
		List<MerchantSummaryDailyTiny> result = new ArrayList<MerchantSummaryDailyTiny>();
		try{
			Query query = session.createSQLQuery("exec SP_GetMerchantSummaryForAgent_Default :AgentCode").addEntity(MerchantSummaryDailyTiny.class).setParameter("AgentCode", agentCode);
			result =(List<MerchantSummaryDailyTiny>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
    }
	
	public List<MerchantSummaryDailyTiny> FindMerchantSummaryForAgentElement(String agentCode,String searchString)
    {
		Session session = getSession();
		List<MerchantSummaryDailyTiny> result = new ArrayList<MerchantSummaryDailyTiny>();
		try{
			Query query = session.createSQLQuery("exec SP_FindMerchantSummaryElementByAgent :Element, :AgentCode").addEntity(MerchantSummaryDailyTiny.class).setParameter("Element", searchString).setParameter("AgentCode", agentCode);
			result =(List<MerchantSummaryDailyTiny>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
    }
}
