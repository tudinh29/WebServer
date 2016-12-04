package com.hcmus.model.pojo;

import org.hibernate.Hibernate;
import org.hibernate.proxy.HibernateProxy;

public class ORMUtils {
	
		    public static <T> T initializeAndUnproxy(T entity) {
		        if (Hibernate.isInitialized(entity)== false) {
		            return null;
		        }
		        if (entity instanceof HibernateProxy) {
		            Hibernate.initialize(entity);
		            entity = (T) ((HibernateProxy) entity).getHibernateLazyInitializer().getImplementation();
		        }
		        return entity;
		    }
		

}
