package com.hcmus.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.TransactionDetail;
//import com.hcmus.model.service.MerchantService;
import com.hcmus.model.service.TransactionDetailService;

@RestController
public class TransactionDetailController {
	private TransactionDetailService transactionDetailService;// = (MerchantService)appCtx.getBean("MerchantService");

	@Autowired(required=true)
	@Qualifier(value="TransactionDetailService")
	public void setTransactionDetailService(TransactionDetailService transactionDetailService) {
		this.transactionDetailService = transactionDetailService;
	}
	@RequestMapping(value = "/TransactionDetail/list", method = RequestMethod.GET, produces = "application/json")  
	public TransactionDetail FindTransactionDetail(){
		TransactionDetail tran = transactionDetailService.FindTransactionDetail("0003IGF7OU608ARQ");
		return tran;
	}
}
