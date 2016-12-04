package com.hcmus.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.Agent;
import com.hcmus.model.service.AgentService;
@RestController
public class AgentController {
	private AgentService agentService;

	@Autowired(required=true)
	@Qualifier(value="AgentService")
	public void setAgentService(AgentService AgentService) {
		this.agentService = AgentService;
	}
	
	@RequestMapping(value = "/Agent/list", method = RequestMethod.GET, produces = "application/json")  
	public List<Agent> getAllAgent() { 
		List<Agent> agents = (List<Agent>)agentService.findAllAgent();
		return agents;
	}
}
