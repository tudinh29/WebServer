package com.hcmus.model.service;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.transform.Transformers;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.hcmus.model.pojo.MerchantSummaryDaily;
import com.hcmus.model.pojo.MerchantSummaryDailyTiny;
import com.hcmus.model.pojo.Statistic;
//@Service("MerchantService")
//extends HibernateDaoSupport
@Transactional
@Service
public class StatisticService  {
	private SessionFactory sessionFactory;
	
	public void setSessionFactory(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}
	private Session getSession() {
		return sessionFactory.openSession();
	}
	
	public List<Statistic> GetReportDateForLineChart()
    {
		Session session = getSession();
		List<Statistic> result = new ArrayList<Statistic>();
		try{
			Query query = session.getNamedQuery("SP_GetReportDataForLineChart_Default").setResultTransformer(Transformers.aliasToBean(Statistic.class));
			result = (List<Statistic>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
    }
	
	/*List<MyBean> list = sessionFactory.getCurrentSession()
            .getNamedQuery("mySp")
            .setParameter("name", name)
            .setResultTransformer(Transformers.aliasToBean(MyBean.class))
            .list();*/
	
	public List<Statistic> GetReportDateForLineChartGenerality(String startDate, String endDate)
    {
		Session session = getSession();
		List<Statistic> result = new ArrayList<Statistic>();
		try{
			Query query = session.getNamedQuery("SP_GetReportDataForLineChart_Generality").setParameter("startDate", startDate).setParameter("endDate", endDate).setResultTransformer(Transformers.aliasToBean(Statistic.class));
			result = (List<Statistic>)query.list();
		} catch(HibernateException ex){
			System.out.println(ex.toString());
		} finally{
			session.close();
		}
		return result;
    }
}