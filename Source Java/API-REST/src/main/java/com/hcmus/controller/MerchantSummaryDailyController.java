package com.hcmus.controller;


import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.hcmus.model.pojo.MerchantSummaryDaily;
import com.hcmus.model.pojo.MerchantSummaryDailyTiny;
import com.hcmus.model.pojo.Statistic;
import com.hcmus.model.service.MerchantSummaryDailyService;
import com.hcmus.model.service.MerchantSummaryDailyTinyService;
import com.hcmus.model.service.StatisticService;

@RestController
public class MerchantSummaryDailyController {
	//ApplicationContext appCtx = new ClassPathXmlApplicationContext("classpath:/Beans/beans-servlet.xml");
	private MerchantSummaryDailyService merchantSummaryDailyService;// = (MerchantService)appCtx.getBean("MerchantService");
	private MerchantSummaryDailyTinyService merchantSummaryDailyTinyService;
	private StatisticService statisticService;
	
	@Autowired(required=true)
	@Qualifier(value="MerchantSummaryDailyService")
	public void setMerchantService(MerchantSummaryDailyService merchantSummaryDailyService) {
		this.merchantSummaryDailyService = merchantSummaryDailyService;
	}
	
	@Autowired(required=true)
	@Qualifier(value="MerchantSummaryDailyTinyService")
	public void setMerchantService(MerchantSummaryDailyTinyService merchantSummaryDailyTinyService) {
		this.merchantSummaryDailyTinyService = merchantSummaryDailyTinyService;
	}
	
	@Autowired(required=true)
	@Qualifier(value="StatisticService")
	public void setMerchantService(StatisticService statisticService) {
		this.statisticService = statisticService;
	}

	@RequestMapping(value = "/MerchantSummaryDaily/{startDate}/{endDate}", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDaily> getReportData_Generality(@PathVariable(value = "startDate")String startDate, @PathVariable(value = "endDate")String endDate) {
		List<MerchantSummaryDaily> summary = merchantSummaryDailyService.GetReportDataGenerality(startDate, endDate);
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/ReportDataDefault", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDaily> GetReportData() {
		List<MerchantSummaryDaily> summary = merchantSummaryDailyService.GetReportData();
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/GetMerchantSummaryDaily", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDaily> GetMerchantSummaryDaily() {
		List<MerchantSummaryDaily> summary = merchantSummaryDailyService.GetMerchantSummaryDaily();
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/GetMerchantSummaryDaily/{Date}", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDaily> GetMerchantSummaryDaily(@PathVariable(value = "Date")String Date) {
		List<MerchantSummaryDaily> summary = merchantSummaryDailyService.GetMerchantSummaryDaily(Date);
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/MerchantSummaryDefault", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDailyTiny> GetMerchantSummaryDefault() {
		List<MerchantSummaryDailyTiny> summary = merchantSummaryDailyTinyService.GetMerchantSummaryDefault();
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/FindMerchantSummaryElement/{searchString}", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDailyTiny> FindMerchantSummaryElement(@PathVariable(value = "searchString")String searchString) {
		List<MerchantSummaryDailyTiny> summary = merchantSummaryDailyTinyService.FindMerchantSummaryElement(searchString);
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/GetMerchantSummaryForAgent/{agentCode}", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDailyTiny> GetMerchantSummaryForAgentDefault(@PathVariable(value = "agentCode")String agentCode) {
		List<MerchantSummaryDailyTiny> summary = merchantSummaryDailyTinyService.GetMerchantSummaryForAgentDefault(agentCode);
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/FindMerchantSummaryForAgentElement/{agentCode}/{searchString}", method = RequestMethod.GET, produces = "application/json")
	public List<MerchantSummaryDailyTiny> GetMerchantSummaryForAgentDefault(@PathVariable(value = "searchString")String searchString, @PathVariable(value = "agentCode")String agentCode) {
		List<MerchantSummaryDailyTiny> summary = merchantSummaryDailyTinyService.FindMerchantSummaryForAgentElement(agentCode, searchString);
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/GetReportDateForLineChart", method = RequestMethod.GET, produces = "application/json")
	public List<Statistic> GetMerchantSummaryForAgentDefault() {
		List<Statistic> summary = statisticService.GetReportDateForLineChart();
	    return summary;
	}
	
	@RequestMapping(value = "/MerchantSummaryDaily/GetReportDateForLineChartGenerality/{startDate}/{endDate}", method = RequestMethod.GET, produces = "application/json")
	public List<Statistic> GetReportDateForLineChartGenerality(@PathVariable(value = "startDate")String startDate, @PathVariable(value = "endDate")String endDate) {
		List<Statistic> summary = statisticService.GetReportDateForLineChartGenerality(startDate, endDate);
	    return summary;
	}
}