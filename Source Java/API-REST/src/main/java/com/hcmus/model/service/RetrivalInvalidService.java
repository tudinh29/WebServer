package com.hcmus.model.service;

import java.util.ArrayList;
import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.hcmus.model.pojo.RetrivalInvalid;
//@Service("MerchantService")
//extends HibernateDaoSupport
@Transactional
@Service

public class RetrivalInvalidService {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	
	public List<RetrivalInvalid> findByRetrivalInvalidCode(String RetrivalCode){
		Session session = getSession();
		List<RetrivalInvalid> result = new ArrayList<RetrivalInvalid>();
		try{
			Query query = session.createSQLQuery("exec sp_FindRetrivalInvalid :RetrivalInvalid ").addEntity(RetrivalInvalid.class).setParameter("RetrivalCode", RetrivalCode);
			result = (List<RetrivalInvalid>)query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
	public List<RetrivalInvalid> findAllRetrivalInvalid(){
		Session session = getSession();
		List<RetrivalInvalid> result = new ArrayList<RetrivalInvalid>();
		try{
			Query query = session.createSQLQuery("exec sp_GetAllRetrivalInvalid").addEntity(RetrivalInvalid.class);
			result = (List<RetrivalInvalid>)query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
	
}
