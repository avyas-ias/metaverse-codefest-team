package com.example.metaversecodefestadserver.controllers.dto.response;

import lombok.Data;

@Data
public class ReportLogDTO {

    private final String sessionId;
    private final Integer viewCountTillNow;
    private final float viewTimeTillNow;

}
