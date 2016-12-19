package com.hcmus.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.Region;
import com.hcmus.model.service.RegionService;
@RestController
public class RegionController {
	private RegionService regionService;// = (MerchantService)appCtx.getBean("MerchantService");

	@Autowired(required=true)
	@Qualifier(value="RegionService")
	public void setMerchantService(RegionService regionService) {
		this.regionService = regionService;
	}
	
	@RequestMapping(value = "/Region/list", method = RequestMethod.GET, produces = "application/json")  
	public List<Region> getAllMerchant() { 
		List<Region> regions = regionService.findAllRegion();
		return regions;
	}
}
