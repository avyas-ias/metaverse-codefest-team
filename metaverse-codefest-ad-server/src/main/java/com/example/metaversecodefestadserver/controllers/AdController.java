package com.example.metaversecodefestadserver.controllers;

import com.example.metaversecodefestadserver.controllers.dto.request.AdPublisherContextDTO;
import com.example.metaversecodefestadserver.controllers.dto.response.AdResponseDTO;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@Slf4j
@RestController
public class AdController {

    @PostMapping(value = "/ad", consumes = MediaType.APPLICATION_JSON_VALUE, produces = MediaType.APPLICATION_JSON_VALUE)
    public AdResponseDTO getSession(@RequestBody final AdPublisherContextDTO contextDto) {
//        log.info("Serving ad to {}", contextDto.getPublisherId());
//        log.info("Context to serve: {}", contextDto);
        return new AdResponseDTO("1", "");
    }

}