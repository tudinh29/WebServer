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

import com.hcmus.model.pojo.City;
//@Service("MerchantService")
//extends HibernateDaoSupport
@Transactional
@Service
public class CityService  {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	
	public List<City> findAllCity(){
		Session session = getSession();
		List<City> result = new ArrayList<City>();
		try{
			Query query = session.createSQLQuery("exec sp_SelectAllCity").addEntity(City.class);
			result = (List<City>)query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
}
