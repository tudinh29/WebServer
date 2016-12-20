package com.hcmus.controller;


import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.Retrival;
import com.hcmus.model.service.RetrivalService;


@RestController
public class RetrivalController {
	//ApplicationContext appCtx = new ClassPathXmlApplicationContext("classpath:/Beans/beans-servlet.xml");
	private RetrivalService retrivalService;// = (RetrivalService)appCtx.getBean("RetrivalService");

	@Autowired(required=true)
	@Qualifier(value="RetrivalService")
	public void setRetrivalService(RetrivalService retrivalService) {
		this.retrivalService = retrivalService;
	}

	@RequestMapping(value = "/Retrival/{retrivalCode}", method = RequestMethod.GET, produces = "application/json")  
	public Retrival getRetrivalById(@PathVariable String retrivalCode) { 
		List<Retrival> retrivals = retrivalService.findByRetrivalElement(retrivalCode);
		if (retrivals.isEmpty()){
			return null;
		}
		else {
			return retrivals.get(0);
		}
	}
	@RequestMapping(value = "/Retrival/list", method = RequestMethod.GET, produces = "application/json")  
	public List<Retrival> getAllRetrival() { 
		List<Retrival> retrivals = retrivalService.findAllRetrival();
		return retrivals;
	}
}
