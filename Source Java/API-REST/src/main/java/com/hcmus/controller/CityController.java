package com.hcmus.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.City;
import com.hcmus.model.service.CityService;
@RestController
public class CityController {
	private CityService cityService;

	@Autowired(required=true)
	@Qualifier(value="CityService")
	public void setCityService(CityService CityService) {
		this.cityService = CityService;
	}
	
	@RequestMapping(value = "/City/list", method = RequestMethod.GET, produces = "application/json")  
	public List<City> getAllCity() { 
		List<City> citys = (List<City>)cityService.findAllCity();
		return citys;
	}
}
