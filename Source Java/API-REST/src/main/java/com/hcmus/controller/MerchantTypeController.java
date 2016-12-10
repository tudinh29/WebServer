package com.hcmus.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;
import com.hcmus.model.pojo.MerchantType;
import com.hcmus.model.service.MerchantTypeService;

@RestController

public class MerchantTypeController {
	private MerchantTypeService merchantTypeService;
	@Autowired(required=true)
	@Qualifier(value="MerchantTypeService")
	public void setMerchantTypeService(MerchantTypeService merchantTypeService) {
		this.merchantTypeService = merchantTypeService;
	}
	@RequestMapping(value = "/MerchantType/list", method = RequestMethod.GET, produces = "application/json")  
	public List<MerchantType> SelectAllMerchantType() { 
		List<MerchantType> list = merchantTypeService.SelectAllMerchantType();
		return list;
	}
	@RequestMapping(value = "/MerchantType/update_{id}_{Desc}", method = RequestMethod.GET, produces = "application/json")  
	public boolean PutMERCHANT_TYPE(@PathVariable String id,@PathVariable String Desc){
		MerchantType m = new MerchantType();
		m.setMerchantType(id);
		m.setDescription(Desc);
		return merchantTypeService.PutMERCHANT_TYPE(id, m);
	}
	@RequestMapping(value = "/MerchantType/delete_{id}", method = RequestMethod.GET, produces = "application/json")  
	public boolean DeleteMERCHANT_TYPE(@PathVariable String id){
		return merchantTypeService.DeleteMERCHANT_TYPE(id);
	}
	@RequestMapping(value = "/MerchantType/insert_{id}_{Desc}", method = RequestMethod.GET, produces = "application/json")  
	public boolean PostMERCHANT_TYPE(@PathVariable String id, @PathVariable String Desc){
		MerchantType m = new MerchantType();
		m.setMerchantType(id);
		m.setDescription(Desc);
		return merchantTypeService.PostMERCHANT_TYPE(m);
	}
	
}
