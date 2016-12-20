package com.hcmus.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.RetrivalInvalid;
import com.hcmus.model.service.RetrivalInvalidService;

@RestController
public class RetrivalInvalidController {
	//ApplicationContext appCtx = new ClassPathXmlApplicationContext("classpath:/Beans/beans-servlet.xml");
		private RetrivalInvalidService retrivalInvalidService;// = (MerchantService)appCtx.getBean("MerchantService");

		@Autowired(required=true)
		@Qualifier(value="RetrivalInvalidService")
		public void setRetrivalInvalidService(RetrivalInvalidService retrivalInvalidService) {
			this.retrivalInvalidService = retrivalInvalidService;
		}

		@RequestMapping(value = "/RetrivalInvalid/{retrivalInvalidCode}", method = RequestMethod.GET, produces = "application/json")  
		public RetrivalInvalid getRetrivalInvalidById(@PathVariable String retrivalInvalidCode) { 
			List<RetrivalInvalid> retrivalInvalids = retrivalInvalidService.findByRetrivalInvalidCode(retrivalInvalidCode);
			if (retrivalInvalids.isEmpty()){
				return null;
			}
			else {
				return retrivalInvalids.get(0);
			}
		}
		@RequestMapping(value = "/RetrivalInvalid/list", method = RequestMethod.GET, produces = "application/json")  
		public List<RetrivalInvalid> getAllRetrivalInvalid() { 
			List<RetrivalInvalid> retrivalInvalid = retrivalInvalidService.findAllRetrivalInvalid();
			return retrivalInvalid;
		}
}
