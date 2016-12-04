package com.hcmus.model.service;

import java.util.ArrayList;
import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.transaction.annotation.Transactional;

import com.hcmus.model.pojo.Agent;
@Transactional
public class AgentService {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	public List<Agent> findAllAgent(){
		Session session = getSession();
		List<Agent> result = new ArrayList<Agent>();
		try{
			Query query = session.createSQLQuery("exec sp_FindAllAgent").addEntity(Agent.class);
			result = (List<Agent>)query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
}
