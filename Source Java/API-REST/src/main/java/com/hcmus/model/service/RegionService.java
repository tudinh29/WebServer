package com.hcmus.model.service;

import java.util.ArrayList;
import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.transaction.annotation.Transactional;

import com.hcmus.model.pojo.Region;
@Transactional
public class RegionService {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	public List<Region> findAllRegion(){
		Session session = getSession();
		List<Region> result = new ArrayList<Region>();
		try{
			Query query = session.createSQLQuery("exec sp_FindAllRegion").addEntity(Region.class);
			result = (List<Region>)query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}

}
