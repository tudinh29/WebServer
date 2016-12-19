package com.hcmus.controller;


import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.Merchant;
import com.hcmus.model.service.MerchantService;

@RestController
public class MerchantController {
	//ApplicationContext appCtx = new ClassPathXmlApplicationContext("classpath:/Beans/beans-servlet.xml");
	private MerchantService merchantService;// = (MerchantService)appCtx.getBean("MerchantService");

	@Autowired(required=true)
	@Qualifier(value="MerchantService")
	public void setMerchantService(MerchantService merchantService) {
		this.merchantService = merchantService;
	}

	@RequestMapping(value = "/Merchant/{merchantCode}", method = RequestMethod.GET, produces = "application/json")  
	public Merchant getMerchantById(@PathVariable String merchantCode) { 
		List<Merchant> merchants = merchantService.findByMerchantCode(merchantCode);
		if (merchants.isEmpty())
			return null;
		return merchants.get(0);
	}
	@RequestMapping(value = "/Merchant/list", method = RequestMethod.GET, produces = "application/json")  
	public List<Merchant> getAllMerchant() { 
		List<Merchant> merchants = merchantService.findAllMerchant();
		return merchants;
	}
}
