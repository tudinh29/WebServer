package com.hcmus.model.service;

import java.util.ArrayList;
import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import com.hcmus.model.pojo.Retrival;

//@Service("RetrivalService")
//extends HibernateDaoSupport
@Transactional
@Service
public class RetrivalService  {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	
	public List<Retrival> findByRetrivalElement(String RetrivalCode){
		Session session = getSession();
		List<Retrival> result = new ArrayList<Retrival>();
		try{
			Query query = session.createSQLQuery("exec sp_FindRetrivalElement :RetrivalCode ").addEntity(Retrival.class).setParameter("RetrivalCode", RetrivalCode);
			result = query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
	
	
	public List<Retrival> findAllRetrival(){
		Session session = getSession();
		List<Retrival> result = new ArrayList<Retrival>();
		try{
			Query query = session.createSQLQuery("exec sp_FindAllRetrival").addEntity(Retrival.class);
			result = query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
}
