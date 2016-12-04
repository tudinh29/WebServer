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
//@Service("MerchantService")
//extends HibernateDaoSupport
@Transactional
@Service
public class MerchantService  {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	public List<Merchant> findByMerchantCode(String MerchantCode){
		Session session = getSession();
		List<Merchant> result = new ArrayList<Merchant>();
		try{
			Query query = session.createSQLQuery("exec sp_GetMerchant :MerchantCode ").addEntity(Merchant.class).setParameter("MerchantCode", MerchantCode);
			result = (List<Merchant>)query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
	public List<Merchant> findAllMerchant(){
		Session session = getSession();
		List<Merchant> result = new ArrayList<Merchant>();
		try{
			Query query = session.createSQLQuery("exec sp_FindAllMerchant").addEntity(Merchant.class);
			result = (List<Merchant>)query.list();
			
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
	}
}
