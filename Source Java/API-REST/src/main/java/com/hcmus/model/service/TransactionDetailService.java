package com.hcmus.model.service;

import java.util.List;

import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;

import com.hcmus.model.pojo.Merchant;
import com.hcmus.model.pojo.TransactionDetail;

public class TransactionDetailService {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	public TransactionDetail FindTransactionDetail(String TransactionCode){
		Session session = getSession();
		Query query = session.createSQLQuery("exec sp_findTransactionDetail").addEntity(TransactionDetail.class);
		List<TransactionDetail> result = (List<TransactionDetail>)query.list();
				
		TransactionDetail tran = result.get(0);
		return tran;

	}
}
